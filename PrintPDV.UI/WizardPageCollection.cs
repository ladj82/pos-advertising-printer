using System.Collections.Generic;
using System.Linq;

namespace PrintPDV.UI
{
    public enum WizardPageLocation
    {
        Start,
        Middle,
        End
    }

    public class WizardPageCollection : Dictionary<int, IWizardStep>
    {
        #region Properties

        /// <summary>
        /// The current IWizardStep
        /// </summary>
        public IWizardStep CurrentPage { get; private set; }
        
        /// <summary>
        /// The first page in the collection
        /// </summary>
        public IWizardStep FirstPage
        {
            get { return this[this.Min(x => x.Key)]; }
        }
        
        /// <summary>
        /// The last page in the collection
        /// </summary>
        public IWizardStep LastPage
        {
            get { return this[this.Max(x => x.Key)]; }
        }

        /// <summary>
        /// The location of the current IWizardStep
        /// </summary>
        public WizardPageLocation PageLocation { get; private set; }

        /// <summary>
        /// <para>Determines whether the wizard is able to move to the next page.</para>
        /// <para>Will return false if Page Location is currently the last page.</para>
        /// <para>Otherwise, true.</para>
        /// </summary>
        public bool CanMoveNext
        {
            get
            {
                if (this.Count == 1)
                {
                    return false;
                }

                if (this.Count > 0 && this.PageLocation != WizardPageLocation.End)
                {
                    return true;
                }

                return false;
            }
        }
        /// <summary>
        /// <para>Determines whether the wizard is able to move to the previous page.</para>
        /// <para>Will return false if Page Location is currently the first page.</para>
        /// <para>Otherwise, true.</para>
        /// </summary>
        public bool CanMovePrevious
        {
            get
            {
                if (this.Count == 1)
                {
                    return false;
                }

                if (this.Count > 0 && this.PageLocation != WizardPageLocation.Start)
                {
                    return true;
                }

                return false;
            }
        }

        #endregion

        #region Constructor

        public WizardPageCollection()
        {
            PageLocation = WizardPageLocation.Start;
        }

        #endregion

        #region Delegates & Events

        public delegate void WizardPageLocationChangedEventHandler(WizardPageLocationChangedEventArgs e);
        public event WizardPageLocationChangedEventHandler WizardPageLocationChanged;

        #endregion

        #region Public Methods

        /// <summary>
        /// Moves to the first page in the collection
        /// </summary>
        /// <returns>First page as IWizard</returns>
        public IWizardStep MovePageFirst()
        {
            var previousPageIndex = IndexOf(CurrentPage);

            PageLocation = WizardPageLocation.Start;
            // Find the index of the first page
            var firstPageIndex = (from x in this
                                  select x.Key).Min();

            // Set the current page to be the first page
            CurrentPage = this[firstPageIndex];

            NotifyPageChanged(previousPageIndex);

            return CurrentPage;
        }

        /// <summary>
        /// Moves to the last page in the collection
        /// </summary>
        /// <returns>Last page as IWizard</returns>
        public IWizardStep MovePageLast()
        {
            var previousPageIndex = IndexOf(CurrentPage);

            PageLocation = WizardPageLocation.End;
            // Find the index of the last page
            var lastPageIndex = (from x in this
                                 select x.Key).Max();

            // Set the current page to be the last page
            CurrentPage = this[lastPageIndex];

            NotifyPageChanged(previousPageIndex);

            return CurrentPage;
        }

        /// <summary>
        /// Moves to the next page in the collection
        /// </summary>
        /// <returns>Next page as IWizard</returns>
        public IWizardStep MovePageNext()
        {
            var previousPageIndex = IndexOf(CurrentPage);

            if (PageLocation != WizardPageLocation.End && CurrentPage != null)
            {
                // Find the index of the next page
                var nextPageIndex = (from x in this
                                     where x.Key > IndexOf(CurrentPage)
                                     select x.Key).Min();

                // Find the index of the last page
                var lastPageIndex = (from x in this
                                     select x.Key).Max();

                // If the next page is the last page
                PageLocation = nextPageIndex == lastPageIndex ? WizardPageLocation.End : WizardPageLocation.Middle;

                // Set the current page to be the next page                
                CurrentPage = this[nextPageIndex];

                NotifyPageChanged(previousPageIndex);

                return CurrentPage;
            }

            return null;
        }
        /// <summary>
        /// Moves to the previous page in the collection
        /// </summary>
        /// <returns>Previous page as IWizard</returns>
        public IWizardStep MovePagePrevious()
        {
            var prevPageIndex = IndexOf(CurrentPage);

            if (PageLocation != WizardPageLocation.Start && CurrentPage != null)
            {
                // Find the index of the previous page
                var previousPageIndex = (from x in this
                                         where x.Key < IndexOf(CurrentPage)
                                         select x.Key).Max();

                // Find the index of the first page
                var firstPageIndex = (from x in this
                                      select x.Key).Min();

                // If the previous page is the first page
                PageLocation = previousPageIndex == firstPageIndex ? WizardPageLocation.Start : WizardPageLocation.Middle;

                CurrentPage = this[previousPageIndex];

                NotifyPageChanged(prevPageIndex);

                return CurrentPage;
            }

            return null;
        }

        public IWizardStep MovePagePending()
        {
            var previousPageIndex = IndexOf(CurrentPage);

            var pendingPageIndex = this.First(z => z.Value.PageValid.Equals(false)).Key;

            var firstPageIndex = (from x in this
                                  select x.Key).Min();

            var lastPageIndex = (from x in this
                                 select x.Key).Max();

            if (pendingPageIndex.Equals(firstPageIndex))
            {
                PageLocation = WizardPageLocation.Start;
            }
            else if (pendingPageIndex.Equals(lastPageIndex))
            {
                PageLocation = WizardPageLocation.End;
            }
            else
            {
                PageLocation = WizardPageLocation.Middle;
            }

            CurrentPage = this[pendingPageIndex];

            NotifyPageChanged(previousPageIndex);

            return CurrentPage;
        }

        /// <summary>
        /// Find the page number of the current page
        /// </summary>
        /// <param name="wizardPage">The IWiwardPage whose page number to retrieve.</param>
        /// <returns>Page number for the given IWizardStep</returns>
        public int IndexOf(IWizardStep wizardPage)
        {
            foreach (var kv in this.Where(kv => kv.Value.Equals(wizardPage)))
            {
                return kv.Key;
            }

            return -1;
        }

        public void Reset()
        {
            CurrentPage = null;
            PageLocation = WizardPageLocation.Start;
        }

        #endregion

        #region private Methods

        private void NotifyPageChanged(int previousPageIndex)
        {
            if (WizardPageLocationChanged == null) return;

            var e = new WizardPageLocationChangedEventArgs
            {
                PageLocation = PageLocation,
                PageIndex = IndexOf(CurrentPage),
                PreviousPageIndex = previousPageIndex
            };

            WizardPageLocationChanged(e);
        }

        #endregion
    }

    public class WizardPageLocationChangedEventArgs
    {
        /// <summary>
        /// The location of the current IWizardStep
        /// </summary>
        public WizardPageLocation PageLocation { get; set; }

        /// <summary>
        /// The page number of the current IWizardStep
        /// </summary>
        public int PageIndex { get; set; }

        public int PreviousPageIndex { get; set; }
    }
}
