using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
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

        private IWebElement subjectHeadingDropDown => webDriver.FindElement(By.Name("id_contact"));
        private IWebElement contactEmailTextBox => webDriver.FindElement(By.Id("email"));
        private IWebElement orderReferenceTextBox => webDriver.FindElement(By.Name("id_order"));
        private IWebElement fileUploadTextBox => webDriver.FindElement(By.Name("MAX_FILE_SIZE"));
        private IWebElement fileUploadButton => webDriver.FindElement(By.Name("fileUpload"));
        private IWebElement messageTextArea => webDriver.FindElement(By.Name("message"));
        private IWebElement sendMessageButton => webDriver.FindElement(By.Name("submitMessage"));
        private IWebElement contactUsLink => webDriver.FindElement(By.Id("contact-link"));
        private IWebElement contactMessageSuccessNotification => webDriver.FindElement(By.CssSelector("p.alert.alert-success"));

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
        public void Given_ExistingSearchItemName_When_Searching_ThenReturn_SearchResultsCountGreatorThanZero()
        {
            searchTextBox.SendKeys("Printed dress");
            searchButton.Click();

            var expected = "Showing 1 - 5 of 5 items";
            var actual = webDriver.FindElement(By.ClassName("product-count")).Text;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Given_ValidContactDetails_When_SendingContactMessage_ThenReturns_ContactSuccessMessage()
        {
            contactUsLink.Click();
            subjectHeadingDropDown.FindElement(By.XPath("//option[. = 'Customer service']")).Click();
            contactEmailTextBox.SendKeys("mbatha@gmail.com");
            orderReferenceTextBox.SendKeys("700");
            messageTextArea.SendKeys("My order is missing some items");
            sendMessageButton.Click();

            Assert.IsTrue(contactMessageSuccessNotification.Displayed);
        }

        [Test]
        public void GivenValidSignInDetails_When_SigningIn_ThenReturns_SignIngSuccesfullMessage()
        {
            signInLink.Click();
            signEmailTextBox.SendKeys("mbathatest@gmail.com");
            signPasswordTextBox.SendKeys("123456789");
            signInButton.Click();

            Assert.IsTrue(signOutLink.Displayed);
            signOutLink.Click();
        }

        [TearDown]
        public void TearDown()
        {
            webDriver.Dispose();
        }

    }

}
