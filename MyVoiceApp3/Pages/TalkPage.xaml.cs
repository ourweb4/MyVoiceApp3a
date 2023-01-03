﻿using Microsoft.Maui;
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

    public partial class TalkPage : ContentPage
    {
        /// <summary>
        /// The database
        /// </summary>
        private Database db = new Database();

        /// <summary>
        /// The words
        /// </summary>
        private IList<Word> words;

        private bool autotalk = true;

        public TalkPage()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Handles the OnItemSelected event of the ListWords control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="SelectedItemChangedEventArgs"/> instance containing the event data.</param>
        private void ListWords_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Word word = e.SelectedItem as Word;
            etext.Text += " " + word.Phrase;
            if (autotalk)
            {


                SpeakNow(word.Phrase);
                etext.Text = "";
            }
        }



        /// <summary>
        /// When overridden, allows application developers to customize behavior immediately prior to the <see cref="T:Xamarin.Forms.Page" /> becoming visible.
        /// </summary>
        /// <remarks>To be added.</remarks>
        protected override void OnAppearing()
        {
            base.OnAppearing();
            ReadDB();
            //        var app = Application.Current as App;
            //        autotalk = app.autoflag;
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
        /// Handles the OnClicked event of the Buttalk control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Buttalk_OnClicked(object sender, EventArgs e)
        {
            string say = etext.Text;

            SpeakNow(say);
            etext.Text = "";

        }

        /// <summary>
        /// Handles the OnItemTapped event of the ListWords control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ItemTappedEventArgs"/> instance containing the event data.</param>
        private void ListWords_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            Word word = e.Item as Word;
            etext.Text += " " + word.Phrase;
            if (autotalk)
            {


                SpeakNow(word.Phrase);
                etext.Text = "";
            }
        }

        /// <summary>
        /// Reads the database.
        /// </summary>
        private void ReadDB()
        {
            words = new List<Word>();

            words = db.ReadWords();

            ListWords.ItemsSource = words;
            //       NewWord();
        }

        private void ButAdd_OnClicked(object sender, EventArgs e)
        {
            string text = etext.Text;
            if (text != "")
            {
                Word word = new Word();
                word.Id = 0;
                word.Phrase = text;
                word.Title = text;
                db.WriteWord(word);
                ReadDB();
            }
        }


    }
}