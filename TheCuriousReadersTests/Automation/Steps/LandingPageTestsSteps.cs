using MySql.Data.MySqlClient;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using RestSharp;
using SpecFlowTemplate.Steps.Actions;
using SpecFlowTemplate.UIComponents;
using SpecFlowTemplate.Utils;
using System;
using System.Data;
using TechTalk.SpecFlow;

namespace TheCuriousReaderTests.Steps
{
    [Binding]
    public class LandingPageTestsSteps
    {
        private readonly BaseUserActions _user;
        private readonly RestActions _api;
        private readonly ScenarioContext _scenarioContext;

        private const string CONTACT_ADDRESS_NAME = "bul. \"Vitosha\" 89B, 1606 Ivan Vazov, Sofia";
        private const string PHONE_NUMBER = "+359885590096";
        private const string LIBRARY_NAME = "The Curious Readers Library";
        private const string NUMBER_OF_READERS_FIELD = "Total count of readers in the library:";
        private const string GENRES_TITLE_TEXT = "The genres you will find in our library";

        public LandingPageTestsSteps(BaseUserActions user, RestActions api, ScenarioContext scenarioContext)
        {
            _user = user;
            _api = api;
            _scenarioContext = scenarioContext;
        }

        [Given(@"the user is on Landing page")]
        public void GivenTheUserIsOnLandingPage()
        {
            _user.OpensPage("Landing");
        }

        [Then(@"the user should see correct contact info in footer")]
        public void ThenTheUserShouldSeeCorrectContactInfoInFooter()
        {
            Assert.Multiple(() =>
            {
                Assert.That(_user.ReadsTextFrom(LandingPage.CONTACT_ADDRESS_FIELD), Is.EqualTo(CONTACT_ADDRESS_NAME));
                Assert.That(_user.ReadsTextFrom(LandingPage.CONTACT_PHONE_FIELD), Is.EqualTo(PHONE_NUMBER));
            });
        }

        [Then(@"the user should see correct name of the library")]
        public void ThenTheUserShouldSeeCorrectNameOfTheLibrary()
        {
            Assert.That(_user.ReadsTextFrom(LandingPage.LIBRARY_NAME_CONTAINER), Is.EqualTo(LIBRARY_NAME));
        }

        [Then(@"the user should see login button")]
        public void ThenTheUserShouldLoginButton()
        {
            bool isLoginButtonDisplayed = WebDriverProvider.Driver.FindElement(By.CssSelector(".header-container > button:nth-of-type(2)")).Displayed;
            Assert.IsTrue(isLoginButtonDisplayed);
        }

        [Then(@"the user should see register button")]
        public void ThenTheUserShouldSeeRegisterButton()
        {
            bool isRegisterButtonDisplayed = WebDriverProvider.Driver.FindElement(By.CssSelector(".header-container > button:nth-of-type(1)")).Displayed;
            Assert.IsTrue(isRegisterButtonDisplayed);
        }


        [Then(@"the user should see total number of books in different genres")]
        public void ThenTheUserShouldSeeTotalNumberOfBooksInDifferentGenres()
        {
            Assert.That(_user.ReadsTextFrom(LandingPage.GENRES_TITLE), Is.EqualTo("The genres you will find in our library"));
        }

        [Then(@"the user should see a newly added books table")]
        public void ThenTheUserShouldSeeANewlyAddedBooksTable()
        {
            Assert.Multiple(() =>
            {
                Assert.That(_user.ReadsTextFrom(LandingPage.NEW_BOOKS_TABLE_TITLE_COLUMN), Is.EqualTo("Title"));
                Assert.That(_user.ReadsTextFrom(LandingPage.NEW_BOOKS_TABLE_DESCRIPTION_COLUMN), Is.EqualTo("Description"));
                Assert.That(_user.ReadsTextFrom(LandingPage.NEW_BOOKS_TABLE_COVER_COLUMN), Is.EqualTo("Cover"));
                Assert.That(_user.ReadsTextFrom(LandingPage.NEW_BOOKS_TABLE_GENRE_COLUMN), Is.EqualTo("Genre"));
                Assert.That(_user.ReadsTextFrom(LandingPage.NEW_BOOKS_TABLE_QUANTITY_COLUMN), Is.EqualTo("Quantity"));
                Assert.That(_user.ReadsTextFrom(LandingPage.NEW_BOOKS_TABLE_AUTHOR_COLUMN), Is.EqualTo("Author name"));
                Assert.That(_user.ReadsTextFrom(LandingPage.NEW_BOOKS_TABLE_ADDED_DATE_COLUMN), Is.EqualTo("Added at"));
            });
        }

        [Then(@"the user should see the number of readers")]
        public void ThenTheUserShouldSeeTheNumberOfReaders()
        {
            string uri = "https://localhost:44385/api/Accounts/total";
            string method = "GET";
            IRestResponse response = _api.sendRequest(uri, method);
            string numberOfReaders = response.Content;
            string fullTitleForNumberOfReaders = $"{NUMBER_OF_READERS_FIELD} {numberOfReaders}";
            Assert.That(_user.ReadsTextFrom(LandingPage.NUMBER_OF_READERS), Is.EqualTo(fullTitleForNumberOfReaders));
        }

        [Then(@"the user should see correct number of readers")]
        public void ThenTheUserShouldSeeCorrectNumberOfReaders()
        {
            string uri = "https://localhost:44385/api/Accounts/total";
            string method = "GET";
            IRestResponse response = _api.sendRequest(uri, method);
            string numberOfReaders = response.Content;
            Assert.That(_user.ReadsTextFrom(LandingPage.NUMBER_OF_READERS).Contains(numberOfReaders));
        }

        [Then(@"the user should see correct number of books")]
        public void ThenTheUserShouldSeeCorrectNumberOfBooks()
        {
            /* This is query that get all books from last 2 weeks
             * select COUNT(Id) from Books where CreatedAt >= DATEADD(DAY, -14, GETDATE());*/
            string bookFromLastTwoWeeksQuery = "select COUNT(Id) from Books where CreatedAt >= DATEADD(DAY, -14, GETDATE());";
            /*DBConnection.InsertRecord(bookFromLastTwoWeeksQuery);*/
            IDbCommand command = new MySqlCommand(bookFromLastTwoWeeksQuery);
            IDataReader reader = command.ExecuteReader();
            Console.WriteLine(reader);
            Console.WriteLine();
        }

        [When(@"new book is added")]
        public void WhenNewBookIsAdded()
        {
            string uri = "https://localhost:44385/api/Books";
            string method = "POST";
            JObject author = new JObject();
            author.Add("name", "Michael Bolton");
            JObject genre = new JObject();
            genre.Add("name", "Crime");
            JObject newBook = new JObject();
            newBook.Add("title", "Test title");
            newBook.Add("description", "Test description");
            newBook.Add("cover", "00000000");
            newBook.Add("quantity", "5");
            newBook.Add("author", author);
            newBook.Add("genre", genre);

            IRestResponse response = _api.sendRequest(uri, method, newBook);

            _scenarioContext.Add("createdBook", response);

        }

        [Then(@"counter for books in this genre should be greater with (.*) book")]
        public void ThenCounterForBooksInThisGenreShouldBeGreaterWithBook(int p0)
        {
            IRestResponse test =_scenarioContext.Get<IRestResponse>("createdBook");
            string id = test.Content;


        }




    }
}
