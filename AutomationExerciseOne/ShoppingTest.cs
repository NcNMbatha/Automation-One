using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.IO;
using System.Reflection;

namespace AutomationExerciseOne
{
    [TestFixture]
    public class ShoppingTest
    {
        private IWebDriver webDriver;
        private WebDriverWait webDriverWait;

        private IWebElement searchTextBox => webDriver.FindElement(By.Name("search_query"));
        private IWebElement searchButton => webDriver.FindElement(By.Name("submit_search"));
        private IWebElement searchResultsMessageAlert => webDriver.FindElement(By.ClassName("product-count"));

        private IWebElement subjectHeadingDropDown => webDriver.FindElement(By.Name("id_contact"));
        private IWebElement contactEmailTextBox => webDriver.FindElement(By.Id("email"));
        private IWebElement orderReferenceTextBox => webDriver.FindElement(By.Name("id_order"));
        private IWebElement fileUploadTextBox => webDriver.FindElement(By.Name("MAX_FILE_SIZE"));
        private IWebElement fileUploadButton => webDriver.FindElement(By.Name("fileUpload"));
        private IWebElement messageTextArea => webDriver.FindElement(By.Name("message"));
        private IWebElement sendMessageButton => webDriver.FindElement(By.Name("submitMessage"));
        private IWebElement contactUsLink => webDriver.FindElement(By.Id("contact-link"));
        private IWebElement contactMessageSuccessAlert => webDriver.FindElement(By.CssSelector("p.alert.alert-success"));

        private IWebElement signEmailTextBox => webDriver.FindElement(By.Name("email"));
        private IWebElement signPasswordTextBox => webDriver.FindElement(By.Name("passwd"));
        private IWebElement signInButton => webDriver.FindElement(By.Name("SubmitLogin"));
        private IWebElement signInLink => webDriver.FindElement(By.CssSelector("a.login"));
        private IWebElement signOutLink => webDriver.FindElement(By.CssSelector("a.logout"));

        [SetUp]
        public void Setup()
        {
            webDriver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
            webDriver.Manage().Window.Maximize();
            webDriver.Navigate().GoToUrl("http://automationpractice.com/");
            webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            webDriverWait = new WebDriverWait(webDriver,TimeSpan.FromSeconds(10));
        }

        [Test]
        public void Given_ExistingSearchItemName_When_Searching_ThenConfirm_SearchResultsMessageAlertIsEqualsToExpected()
        {
            var expected = "Showing 1 - 5 of 5 items";
            var searchInput = "Printed dress";

            searchTextBox.SendKeys(searchInput);
            searchButton.Click();

            Assert.AreEqual(expected, searchResultsMessageAlert.Text);
        }

        [Test]
        public void Given_ValidContactDetails_When_SendingContactMessage_ThenDisplay_ClontactMessageSuccessAlert()
        {
            var contactEmailInput = "mbatha@gmail.com";
            var orderReferenceInput = "700";
            var contactMessageInput = "My order is missing some items";
            var contactSubject = "Customer service";

            contactUsLink.Click();
            var selectDropDownElement = new SelectElement(subjectHeadingDropDown);
            selectDropDownElement.SelectByText(contactSubject);
            contactEmailTextBox.SendKeys(contactEmailInput);
            orderReferenceTextBox.SendKeys(orderReferenceInput);
            messageTextArea.SendKeys(contactMessageInput);
            sendMessageButton.Click();

            Assert.IsTrue(contactMessageSuccessAlert.Displayed);
        }   

        [Test]
        public void GivenValidSignInDetails_When_SigningIn_ThenConfirm_SignOutLinkTextEqualsExpectedLinkText()
        {
            var signInEmailInput = "mbathatest@gmail.com";
            var signInPasswordInput = "123456789";
            var expectedLinkText = "Sign out";
            var signOutErrorMessage = "Failed to logout";

            try
            {
                signInLink.Click();
                signEmailTextBox.SendKeys(signInEmailInput);
                signPasswordTextBox.SendKeys(signInPasswordInput);
                signInButton.Click();
                var signOutLinkText = signOutLink.Text;
                signOutLink.Click();

                if (signInLink.Displayed)
                {
                    Assert.AreEqual(expectedLinkText, signOutLinkText);
                }
            }
            catch (Exception)
            {
                 throw new Exception($"{signOutErrorMessage}");
            }
        }

        [TearDown]
        public void TearDown()
        {
            webDriver.Dispose();
        }

    }

}
