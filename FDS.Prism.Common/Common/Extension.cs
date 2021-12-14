using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace FDS.Prism.Common.Common
{
	public static class Extension
    {
        public static IEnumerable<T> ChildrenOfType<T>(this DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in ChildrenOfType<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }

        public static T FindChildByType<T>(this DependencyObject depObj) where T : DependencyObject
        {
            if (depObj == null) return null;

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
            {
                var child = VisualTreeHelper.GetChild(depObj, i);

                var result = (child as T) ?? FindChildByType<T>(child);
                if (result != null) return result;
            }
            return null;
        }

        public static IEnumerable<T> FindVisualChildren<T>(this DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }

        public static T FindChildByName<T>(this DependencyObject parent, string childName) where T : DependencyObject
        {
            // Confirm parent and childName are valid. 
            if (parent == null) return null;
            T foundChild = null;
            int childrenCount = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < childrenCount; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                // If the child is not of the request child type child
                T childType = child as T;
                if (childType == null)
                {
                    // recursively drill down the tree
                    foundChild = FindChildByName<T>(child, childName);
                    // If the child is found, break so we do not overwrite the found child. 
                    if (foundChild != null) break;
                }
                else if (!string.IsNullOrEmpty(childName))
                {
                    var frameworkElement = child as FrameworkElement;
                    // If the child's name is set for search
                    if (frameworkElement != null && frameworkElement.Name == childName)
                    {
                        // if the child's name is of the request name
                        foundChild = (T)child;
                        break;
                    }
                }
                else
                {
                    // child element found.
                    foundChild = (T)child;
                    break;
                }
            }
            return foundChild;
        }


		public static T FindChildByName2<T>(this DependencyObject parent, string childName) where T : DependencyObject
		{
			// Confirm parent and childName are valid. 
			if (parent == null) return null;
			T foundChild = null;
			int childrenCount = VisualTreeHelper.GetChildrenCount(parent);
			for (int i = 0; i < childrenCount; i++)
			{
				var child = VisualTreeHelper.GetChild(parent, i);
				// If the child is not of the request child type child
				T childType = child as T;
				if (childType == null)
				{
					// recursively drill down the tree
					foundChild = FindChildByName2<T>(child, childName);
					// If the child is found, break so we do not overwrite the found child. 
					if (foundChild != null) break;
				}
				else if (!string.IsNullOrEmpty(childName))
				{
					var frameworkElement = child as FrameworkElement;
					// If the child's name is set for search
					if (frameworkElement != null && frameworkElement.Name == childName)
					{
						// if the child's name is of the request name
						foundChild = (T)child;
						break;
					}
					else
					{
						// recursively drill down the tree
						foundChild = FindChildByName2<T>(child, childName);
						// If the child is found, break so we do not overwrite the found child. 
						if (foundChild != null) break;
					}
				}
				else
				{
					// child element found.
					foundChild = (T)child;
					break;
				}
			}
			return foundChild;
		}

		public static T GetAtPoint<T>(this UIElement reference, MouseEventArgs e) where T : DependencyObject
        {
            return reference.GetAtPoint<T>(e.GetPosition(reference));
        }

        public static T GetAtPoint<T>(this UIElement reference, Point point) where T : DependencyObject
        {
            DependencyObject element = reference == null ? null : reference.InputHitTest(point) as DependencyObject;
            if (element == null)
                return null;
            else if (element is T)
                return (T)element;
            else
                return element.TryFindParent<T>();
        }
        public static T TryFindParent<T>(this DependencyObject child) where T : DependencyObject
        {
            //get parent item
            DependencyObject parentObject = child.GetParentObject();

            //we've reached the end of the tree
            if (parentObject == null)
                return null;

            //check if the parent matches the type we're looking for
            T parent = parentObject as T;
            if (parent != null)
                return parent;
            else
                //use recursion to proceed with next level
                return parentObject.TryFindParent<T>();
        }
        public static DependencyObject GetParentObject(this DependencyObject child)
        {
            if (child == null) return null;

            //handle content elements separately
            ContentElement contentElement = child as ContentElement;
            if (contentElement != null)
            {
                DependencyObject parent = ContentOperations.GetParent(contentElement);
                if (parent != null)
                    return parent;

                FrameworkContentElement fce = contentElement as FrameworkContentElement;
                return fce != null ? fce.Parent : null;
            }

            //also try searching for parent in framework elements (such as DockPanel, etc)
            FrameworkElement frameworkElement = child as FrameworkElement;
            if (frameworkElement != null)
            {
                DependencyObject parent = frameworkElement.Parent;
                if (parent != null)
                    return parent;
            }

            //if it's not a ContentElement/FrameworkElement, rely on VisualTreeHelper
            return VisualTreeHelper.GetParent(child);
        }
        public static IEnumerable<T> GetAllChildItemsByType<T>(this FrameworkElement @this) where T : DependencyObject
        {
            if (@this != null)
            {
                @this.ApplyTemplate();
            }
            else
            {
                return null;
            }

            var result = new List<T>();

            for (int i = 0, count = VisualTreeHelper.GetChildrenCount(@this); i < count; i++)
            {
                var childElement = VisualTreeHelper.GetChild(@this, i) as FrameworkElement;
                var child = childElement as T;

                if (child != null)
                {
                    result.Add(child);
                }
                var childOfChildItem = (childElement as FrameworkElement).GetAllChildItemsByType<T>();

                if (childOfChildItem != null)
                {
                    result.AddRange(childOfChildItem);
                }
            }
            return result as IEnumerable<T>;
        }
    }
}