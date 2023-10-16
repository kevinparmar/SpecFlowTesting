using NUnit.Framework;
using OpenQA.Selenium;
using System;
using TechTalk.SpecFlow;

namespace SpecFlowProjectPizzaShop.StepDefinitions
{
    [Binding]
    public class AddToCartStepDefinitions
    {
        private IWebDriver _driver;
        private string pizzaName;
        public AddToCartStepDefinitions(IWebDriver driver)
        {
            _driver = driver;
        }

        [Given(@"I am a logged-in user")]
        public void GivenIAmALogged_InUser()
        {
            IWebElement welcomeMessage = _driver.FindElement(By.XPath("//*[@id=\"username\"]"));
            Assert.IsTrue(welcomeMessage.Displayed);
            Thread.Sleep(1000);
        }

        [Given(@"I am on a product page")]
        public void GivenIAmOnAProductPage()
        {  
            Assert.AreEqual("https://localhost:7013/", _driver.Url);
            Thread.Sleep(1000);
        }

        [When(@"I click the Add to Cart button")]
        public void WhenIClickTheAddToCartButton()
        {
            IWebElement pizzaContainer = _driver.FindElement(By.XPath("//*[@id=\"Pizza_Container\"]"));

            IWebElement firstPizzaCard = pizzaContainer.FindElement(By.XPath("//*[@id=\"Pizza_Card\"][1]"));
            IWebElement pizzaNameElement = firstPizzaCard.FindElement(By.Id("Pizza_Name"));
            pizzaName = pizzaNameElement.Text;
           
            IWebElement addToCartButton = firstPizzaCard.FindElement(By.XPath("//*[@id=\"AddToCart\"]"));
            Thread.Sleep(1000);
            addToCartButton.Click();   
        }

        [When(@"I click on the cart icon")]
        public void ThenIClickOnTheCartIcon()
        {
            IWebElement Cart = _driver.FindElement(By.XPath("//*[@id=\"Cart\"]"));
            Thread.Sleep(1000);
            Cart.Click();
        }

        [Then(@"I should see the item in the cart")]
        public void ThenIShouldSeeTheItemInTheCart()
        {
            IWebElement cartItemsTable = _driver.FindElement(By.XPath("//*[@id=\"Cart_Items\"]"));
            IList<IWebElement> cartItemNameElements = cartItemsTable.FindElements(By.Id("Cart_Item_Name"));
            bool pizzaNameFound = false;
            foreach (var element in cartItemNameElements)
            {
                if (element.Text.Equals(pizzaName, StringComparison.OrdinalIgnoreCase))
                {
                    pizzaNameFound = true;
                    break;
                }
            }
            Thread.Sleep(1000);
            Assert.IsTrue(pizzaNameFound, $"Pizza with name '{pizzaName}' not found in the table.");
        }

        [Given(@"I am a not logged-in user")]
        public void GivenIAmANotLogged_InUser()
        {
            _driver.Navigate().GoToUrl("https://localhost:7013/");
            IWebElement LoginButton = _driver.FindElement(By.XPath("//*[@id=\"Login_Link\"]"));
            Assert.IsTrue(LoginButton.Displayed);
            Thread.Sleep(1000);
        }

        [Then(@"I should be redirected to the login page")]
        public void ThenIShouldBeRedirectedToTheLoginPage()
        {
            string expectedLoginPageUrl = "https://localhost:7013/Identity/Account/Login";
            string currentUrl = _driver.Url;
            Assert.AreEqual(expectedLoginPageUrl, currentUrl, "Expected to be on the login page.");
            Thread.Sleep(1000);
        }
    }
}
