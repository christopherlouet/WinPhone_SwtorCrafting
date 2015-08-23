using SwtorCrafting.Common;
using SwtorCrafting.Model.View;
using SwtorCrafting.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

// Pour en savoir plus sur le modèle d'élément Page de base, consultez la page http://go.microsoft.com/fwlink/?LinkID=390556

namespace SwtorCrafting.View
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class CraftPlanList : Page
    {
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();
        private List<CraftPlanDetailView> craftPlanDetails = new List<CraftPlanDetailView>();
        private string craftSkill;
        private SwtorcraftingService craftService;

        public CraftPlanList()
        {
            this.InitializeComponent();

            this.craftService = new SwtorcraftingService();
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += this.NavigationHelper_LoadState;
            this.navigationHelper.SaveState += this.NavigationHelper_SaveState;
        }

        /// <summary>
        /// Obtient le <see cref="NavigationHelper"/> associé à ce <see cref="Page"/>.
        /// </summary>
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }

        /// <summary>
        /// Obtient le modèle d'affichage pour ce <see cref="Page"/>.
        /// Cela peut être remplacé par un modèle d'affichage fortement typé.
        /// </summary>
        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }

        /// <summary>
        /// Remplit la page à l'aide du contenu passé lors de la navigation. Tout état enregistré est également
        /// fourni lorsqu'une page est recréée à partir d'une session antérieure.
        /// </summary>
        /// <param name="sender">
        /// La source de l'événement ; en général <see cref="NavigationHelper"/>
        /// </param>
        /// <param name="e">Données d'événement qui fournissent le paramètre de navigation transmis à
        /// <see cref="Frame.Navigate(Type, Object)"/> lors de la requête initiale de cette page et
        /// un dictionnaire d'état conservé par cette page durant une session
        /// antérieure.  L'état n'aura pas la valeur Null lors de la première visite de la page.</param>
        private void NavigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
            string craftSkill = e.NavigationParameter as string;
            if (!string.IsNullOrWhiteSpace(craftSkill))
            {
                this.craftSkill = craftSkill;
                this.loadCraftPlanDetails();
                myCraftPlanList.ItemsSource = craftPlanDetails;
            }
        }

        private async Task loadCraftPlanDetails()
        {
            //await Task.Run(() => craftService.LoadCraftPlanDetails(this.craftSkill));
            craftService.LoadCraftPlanDetails(this.craftSkill);
            craftPlanDetails = craftService.CraftPlanDetails;
        }

        /// <summary>
        /// Conserve l'état associé à cette page en cas de suspension de l'application ou de
        /// suppression de la page du cache de navigation.  Les valeurs doivent être conformes aux
        /// exigences en matière de sérialisation de <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="sender">La source de l'événement ; en général <see cref="NavigationHelper"/></param>
        /// <param name="e">Données d'événement qui fournissent un dictionnaire vide à remplir à l'aide de l'
        /// état sérialisable.</param>
        private void NavigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
        }

        #region Inscription de NavigationHelper

        /// <summary>
        /// Les méthodes fournies dans cette section sont utilisées simplement pour permettre
        /// NavigationHelper pour répondre aux méthodes de navigation de la page.
        /// <para>
        /// La logique spécifique à la page doit être placée dans les gestionnaires d'événements pour  
        /// <see cref="NavigationHelper.LoadState"/>
        /// et <see cref="NavigationHelper.SaveState"/>.
        /// Le paramètre de navigation est disponible dans la méthode LoadState 
        /// en plus de l'état de page conservé durant une session antérieure.
        /// </para>
        /// </summary>
        /// <param name="e">Fournit des données pour les méthodes de navigation et
        /// les gestionnaires d'événements qui ne peuvent pas annuler la requête de navigation.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedTo(e);
        }

        // Display each item incrementally to improve performance.
        private void myCraftPlanList_ContainerContentChanging(
                ListViewBase sender,
                ContainerContentChangingEventArgs args)
        {
            args.Handled = true;

            if (args.Phase != 0)
            {
                throw new Exception("Not in phase 0.");
            }

            // First, show the items' placeholders.
            StackPanel templateRoot =
                (StackPanel)args.ItemContainer.ContentTemplateRoot;
            Rectangle placeholderRectangle =
                (Rectangle)templateRoot.FindName("placeholderRectangle");
            TextBlock itemNameBlock =
                (TextBlock)templateRoot.FindName("itemNameBlock");
            TextBlock itemRarityNameBlock =
                (TextBlock)templateRoot.FindName("itemRarityNameBlock");
            TextBlock itemTypeNameBlock =
                (TextBlock)templateRoot.FindName("itemTypeNameBlock");

            // Make the placeholder rectangle opaque.
            placeholderRectangle.Opacity = 1;

            // Make everything else invisible.
            itemNameBlock.Opacity = 0;
            itemRarityNameBlock.Opacity = 0;
            itemTypeNameBlock.Opacity = 0;

            // Show the items name in the next phase.
            args.RegisterUpdateCallback(ShowItemName);
        }

        // Show the items name.
        private void ShowItemName(
                ListViewBase sender,
                ContainerContentChangingEventArgs args)
        {
            if (args.Phase != 1)
            {
                throw new Exception("Not in phase 1.");
            }

            // Next, show the items' titles. Keep everything else invisible.
            CraftPlanDetailView myItem = (CraftPlanDetailView)args.Item;
            SelectorItem itemContainer =
                (SelectorItem)args.ItemContainer;
            StackPanel templateRoot =
                (StackPanel)itemContainer.ContentTemplateRoot;
            TextBlock titleTextBlock =
                (TextBlock)templateRoot.FindName("itemNameBlock");

            titleTextBlock.Text = myItem.ItemName;
            titleTextBlock.Opacity = 1;

            // Show the items rarity in the next phase.
            args.RegisterUpdateCallback(ShowItemRarityName);
        }

        // Show the items rarity.
        private void ShowItemRarityName(
                ListViewBase sender,
                ContainerContentChangingEventArgs args)
        {
            if (args.Phase != 2)
            {
                throw new Exception("Not in phase 2.");
            }

            // Next, show the items' subtitles. Keep everything else invisible.
            CraftPlanDetailView myItem = (CraftPlanDetailView)args.Item;
            SelectorItem itemContainer = (SelectorItem)args.ItemContainer;

            StackPanel templateRoot =
                (StackPanel)itemContainer.ContentTemplateRoot;
            TextBlock subtitleTextBlock =
                (TextBlock)templateRoot.FindName("itemRarityNameBlock");

            subtitleTextBlock.Text = myItem.ItemRarityName;
            subtitleTextBlock.Opacity = 1;

            // Show the items type name in the next phase.
            args.RegisterUpdateCallback(ShowItemTypeName);
        }

        // Show the items type name.
        private void ShowItemTypeName(
                ListViewBase sender,
                ContainerContentChangingEventArgs args)
        {
            if (args.Phase != 3)
            {
                throw new Exception("Not in phase 3.");
            }

            // Finally, show the items type name. 
            CraftPlanDetailView myItem = (CraftPlanDetailView)args.Item;
            SelectorItem itemContainer = (SelectorItem)args.ItemContainer;

            StackPanel templateRoot =
                (StackPanel)itemContainer.ContentTemplateRoot;
            Rectangle placeholderRectangle =
                (Rectangle)templateRoot.FindName("placeholderRectangle");
            TextBlock descriptionTextBlock =
                (TextBlock)templateRoot.FindName("itemTypeNameBlock");

            descriptionTextBlock.Text = myItem.ItemTypeName;
            descriptionTextBlock.Opacity = 1;

            // Make the placeholder rectangle invisible.
            placeholderRectangle.Opacity = 0;
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedFrom(e);
            //base.OnNavigatedFrom(e);
        }

        #endregion

      
    }
}
