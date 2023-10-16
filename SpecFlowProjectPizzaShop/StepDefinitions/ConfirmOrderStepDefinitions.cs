using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Xml.Linq;
using TechTalk.SpecFlow;

namespace SpecFlowProjectPizzaShop.StepDefinitions
{
    [Binding]
    public class ConfirmOrderStepDefinitions
    {
        private IWebDriver _driver;
        public ConfirmOrderStepDefinitions(IWebDriver driver)
        {
            _driver = driver;
        }

        [Given(@"I am on the Cart Details Page")]
        public void GivenIAmOnTheCartDetailsPage()
        {
            _driver.Navigate().GoToUrl("https://localhost:7013/Cart/Index");
            Thread.Sleep(1000);
        }

        [When(@"I press the Checkout Button")]
        public void WhenIPressTheCheckoutButton()
        {
            IWebElement checkoutButton = _driver.FindElement(By.XPath("//*[@id=\"Checkout_Button\"]"));
            Thread.Sleep(1000);
            checkoutButton.Click();
        }

        [Then(@"I should be on the Order Details Page")]
        public void ThenIShouldBeOnTheOrderDetailsPage()
        {
            Thread.Sleep(1000);
            Assert.AreEqual("https://localhost:7013/Cart/Checkout", _driver.Url);
        }

        [Then(@"I should see the Order Details table")]
        public void ThenIShouldSeeTheOrderDetailsTable()
        {
            IWebElement table = _driver.FindElement(By.XPath("//*[@id=\"Order_Details\"]"));
            Thread.Sleep(1000);
            Assert.IsTrue(table.Displayed);
        }

        [When(@"I press the Confirm Button")]
        public void WhenIPressTheConfirmButton()
        {
            IWebElement confirmButton = _driver.FindElement(By.XPath("//*[@id=\"Confirm_Button\"]"));
            Thread.Sleep(1000);
            ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].click();", confirmButton);
        }

        [Then(@"my order should be confirmed")]
        public void ThenMyOrderShouldBeConfirmed()
        {
            IWebElement confirmationMessage = _driver.FindElement(By.XPath("/html/body/div/main/h1"));
            Thread.Sleep(1000);
            Assert.IsTrue(confirmationMessage.Displayed);
            Assert.IsTrue(confirmationMessage.Text.Contains("Thank you for placing your order"));
        }

        [When(@"I press the Back Button")]
        public void WhenIPressTheBackButton()
        {
            IWebElement backButton = _driver.FindElement(By.XPath("//*[@id=\"Back_Button\"]"));
            ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].click();", backButton);
            Thread.Sleep(1000);
        }

        [Then(@"I should be on the Cart Details Page")]
        public void ThenIShouldBeOnTheCartDetailsPage()
        {
            Thread.Sleep(1000);
            Assert.AreEqual("https://localhost:7013/Cart", _driver.Url);
        }
    }
}
