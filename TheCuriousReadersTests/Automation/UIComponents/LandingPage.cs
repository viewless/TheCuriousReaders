using OpenQA.Selenium;

namespace SpecFlowTemplate.UIComponents
{
    class LandingPage
    {
        public static readonly By EMIAL_FIELD = By.Name("email");

        public static readonly By PASSWORD_FIELD = By.Name("password");

        public static readonly By LOGIN_BUTTON = By.ClassName("login-button");

        public static readonly By ERROR_MESSAGE = By.ClassName("error-popup");

        public static readonly By CONTACT_ADDRESS_FIELD = By.ClassName("contacts-address-main");

        public static readonly By CONTACT_PHONE_FIELD = By.ClassName("contacts-phone-number-main");

        public static readonly By LIBRARY_NAME_CONTAINER = By.CssSelector(".header-title");

        public static readonly By LOGIN_EMAIL_LABEL = By.CssSelector(".header-container [for='email-reg']");

        public static readonly By LOGIN_PASSWORD_LABEL = By.CssSelector(".header-container [for='password-reg']");

        public static readonly By LOGIN_PAGE_BUTTON = By.CssSelector(".header-container > button:nth-of-type(2)");

        public static readonly By NUMBER_OF_READERS = By.ClassName("readers-count");

        public static readonly By GENRES_TITLE = By.CssSelector(".genres-head h2");

        public static readonly By NEW_BOOKS_TABLE_TITLE_COLUMN = By.CssSelector(".newbooks-table thead tr th:nth-of-type(1)");

        public static readonly By NEW_BOOKS_TABLE_DESCRIPTION_COLUMN = By.CssSelector(".newbooks-table thead tr th:nth-of-type(2)");

        public static readonly By NEW_BOOKS_TABLE_COVER_COLUMN = By.CssSelector(".newbooks-table thead tr th:nth-of-type(3)");

        public static readonly By NEW_BOOKS_TABLE_GENRE_COLUMN = By.CssSelector(".newbooks-table thead tr th:nth-of-type(4)");

        public static readonly By NEW_BOOKS_TABLE_QUANTITY_COLUMN = By.CssSelector(".newbooks-table thead tr th:nth-of-type(5)");

        public static readonly By NEW_BOOKS_TABLE_AUTHOR_COLUMN = By.CssSelector(".newbooks-table thead tr th:nth-of-type(6)");

        public static readonly By NEW_BOOKS_TABLE_ADDED_DATE_COLUMN = By.CssSelector(".newbooks-table thead tr th:nth-of-type(7)");


    }
}
