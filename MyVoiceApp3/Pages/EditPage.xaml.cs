using MyVoiceApp3.Models;
using MyVoiceApp3.Utitlys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MyVoiceApp3.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditPage : ContentPage
    {
        /// <summary>
        /// The database
        /// </summary>
        private Database db = new Database();
        /// <summary>
        /// The curr word
        /// </summary>
        private Word currWord = new Word();
        /// <summary>
        /// The words
        /// </summary>
        private IList<Word> words;

        /// <summary>
        /// Initializes a new instance of the <see cref="EditPage" /> class.
        /// </summary>
        public EditPage()
        {
            InitializeComponent();
        }


        /// <summary>
        /// When overridden, allows application developers to customize behavior immediately prior to the <see cref="T:Xamarin.Forms.Page" /> becoming visible.
        /// </summary>
        /// <remarks>To be added.</remarks>
        protected override void OnAppearing()
        {
            base.OnAppearing();
            ReadDB();
        }

        /// <summary>
        /// Loads the form.
        /// </summary>
        private void LoadForm()
        {
            ephrase.Text = currWord.Phrase;
            etitle.Text = currWord.Title;
            eorder.Text = currWord.Order.ToString();
        }

        /// <summary>
        /// Saves the form.
        /// </summary>
        private void SaveForm()
        {
            currWord.Phrase = ephrase.Text;
            currWord.Title = etitle.Text;
            currWord.Order = Convert.ToDecimal(eorder.Text);
        }


        /// <summary>
        /// Reads the database.
        /// </summary>
        private void ReadDB()
        {
            words = new List<Word>();

            words = db.ReadWords();

            ListWords.ItemsSource = words;
            NewWord();
        }

        /// <summary>
        /// News the word.
        /// </summary>
        private void NewWord()
        {
            currWord = new Word();
            currWord.Id = 0;
            LoadForm();
        }

        /// <summary>
        /// Handles the OnClicked event of the MenuNew control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void MenuNew_OnClicked(object sender, EventArgs e)
        {
            NewWord();
        }

        /// <summary>
        /// Handles the OnClicked event of the MenuSave control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void MenuSave_OnClicked(object sender, EventArgs e)
        {
            SaveForm();
            if (currWord.Title != null && currWord.Phrase != null)
                db.WriteWord(currWord);
            ReadDB();
        }


        /// <summary>
        /// Handles the OnClicked event of the MenuDelete control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void MenuDelete_OnClicked(object sender, EventArgs e)
        {
            if (currWord.Id > 0)
            {
                db.DeleteWord(currWord);
                ReadDB();
            }
        }

        /// <summary>
        /// Speaks the now.
        /// </summary>
        /// <param name="words">The words.</param>
        public void SpeakNow(string words)
        {
            TextToSpeech.SpeakAsync(words).ContinueWith((t) =>
            {
                // Logic that will run after utterance finishes.

            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        /// <summary>
        /// Handles the OnClicked event of the Buttest control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void Buttest_OnClicked(object sender, EventArgs e)
        {
            string sayit = ephrase.Text;
            SpeakNow(sayit);
        }

        /// <summary>
        /// Handles the OnItemSelected event of the ListWords control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="SelectedItemChangedEventArgs" /> instance containing the event data.</param>
        private void ListWords_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            currWord = e.SelectedItem as Word;
            LoadForm();
        }

        /// <summary>
        /// Handles the OnItemTapped event of the ListWords control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ItemTappedEventArgs" /> instance containing the event data.</param>
        private void ListWords_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            currWord = e.Item as Word;
            LoadForm();
        }
    }

}