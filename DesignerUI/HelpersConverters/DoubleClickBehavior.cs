using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Interactivity;

namespace webprintDesigner
{
    public class DoubleClickBehavior : Behavior<FrameworkElement>
    {
        #region Constants     /// <summary>
        /// The threshold (in miliseconds) between clicks to be considered a double-click.  Windows default is 500; I'm a fast clicker.    
        /// </summary>    
        private const int ClickThresholdInMiliseconds = 250;
        #endregion
        #region Properties [private]
        /// <summary>    
        /// Holds the timestamp of the last click.    
        /// </summary>    
        private DateTime? LastClick { get; set; }
        /// <summary>    
        /// Holds a reference to the instance of the last source object to generate a click.    
        /// </summary>    
        private object LastSource { get; set; }
        #endregion
        #region Events
        /// <summary>    
        /// The event to be raised upon double-click.    
        /// </summary>    
        public event EventHandler<MouseButtonEventArgs> DoubleClick;


        /// <summary>    
        /// The event to be raised upon single click.    
        /// </summary>    
        public event EventHandler<MouseButtonEventArgs> SingleClick;
        #endregion
        #region Behavior Members [overridden]
        /// <summary>    
        /// This is triggered when the behavior is attached to a FrameworkElement.  An event handler is attached to the    
        /// FrameworkElement's MouseLeftButtonUp event.    
        /// </summary>    
        protected override void OnAttached()
        {
            base.OnAttached();
            this.AssociatedObject.MouseLeftButtonUp += new MouseButtonEventHandler(this.AssociatedObject_MouseLeftButtonUp);
        }

        /// <summary>    
        /// This is triggered when the behavior is detached from a FrameworkElement.  The event handler attached to the MouseLeftButtonUp    
        /// event is removed.    
        /// </summary>   
        protected override void OnDetaching()
        {
            base.OnDetaching();
            this.AssociatedObject.MouseLeftButtonUp -= new MouseButtonEventHandler(this.AssociatedObject_MouseLeftButtonUp);
        }

        #endregion

        #region Event Handlers
        /// <summary>    
        /// Occurs when the MouseLeftButtonDown event is triggered on the object associated to this behavior (this.AssociatedObject).    
        /// </summary>    
        /// <param name="sender">The object which is firing the MouseLeftButtonUp.  Note that this is not always the actual source of the event    
        /// since events are bubbled; this is why we access e.OriginalSource</param>    
        /// <param name="e">The MouseButtonEventArgs associated with the MouseLeftButtonDown event.</param>    
        void AssociatedObject_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (this.LastSource == null || !object.Equals(this.LastSource, e.OriginalSource))
            {
                this.LastSource = e.OriginalSource;
                this.LastClick = DateTime.Now;

                if (this.SingleClick != null)
                    this.SingleClick(sender, e);
            }
            else if ((DateTime.Now - this.LastClick.Value).Milliseconds <= DoubleClickBehavior.ClickThresholdInMiliseconds)
            {
                this.LastClick = null;
                this.LastSource = null;
                if (this.DoubleClick != null)
                    this.DoubleClick(sender, e);
            }
            else
            {
                this.LastClick = null;
                this.LastSource = null;
            }
        }
        #endregion
    }
}
