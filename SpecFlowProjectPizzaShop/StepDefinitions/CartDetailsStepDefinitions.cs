using NUnit.Framework;
using OpenQA.Selenium;
using System;
using TechTalk.SpecFlow;

namespace SpecFlowProjectPizzaShop.StepDefinitions
{
    [Binding]
    public class CartDetailsStepDefinitions
    {
        private IWebDriver _driver;
        private IWebElement tableBody;
        private IList<IWebElement> rows;
        private IWebElement quantityCell;

        private int quantity;

        public CartDetailsStepDefinitions(IWebDriver driver) 
        {
            _driver = driver;
        }

        [Given(@"I am on the Cart Details page")]
        public void GivenIAmOnTheCartDetailsPage()
        {
            _driver.Navigate().GoToUrl("https://localhost:7013/Cart/Index");
            Thread.Sleep(1000);
        }

        [Then(@"the number of unique Cart items should match the cart badge number")]
        public void ThenTheNumberOfUniqueCartItemsShouldMatchTheCartBadgeNumber()
        {
            IWebElement cartBadge = _driver.FindElement(By.XPath("//*[@id=\"cartCount\"]"));
            int cartItemCount = int.Parse(cartBadge.Text);

            IWebElement tableBody = _driver.FindElement(By.XPath("//*[@id=\"Cart_Items_Body\"]"));
            IList<IWebElement> rows = tableBody.FindElements(By.TagName("tr"));

            int rowCount = rows.Count;

            Thread.Sleep(1000);
            Assert.AreEqual(rowCount, cartItemCount);
        }

        [When(@"I increase the quantity of an item in the cart")]
        public void WhenIIncreaseTheQuantityOfAnItemInTheCart()
        {
            updateTableFields();
            updateTableFieldValues();
            IWebElement increaseButton = rows[0].FindElement(By.XPath("//*[@id=\"Increase_Button\"]"));
            Thread.Sleep(1000);
            increaseButton.Click();
        }

        [Then(@"the table should reflect the updated quantity")]
        public void ThenTheTableShouldReflectTheUpdatedQuantity()
        {
            updateTableFields();
            double updatedQuantity = int.Parse(quantityCell.Text);
 
            Thread.Sleep(1000);
            Assert.IsTrue(quantity + 1 == updatedQuantity || quantity == updatedQuantity + 1);
        }

        [When(@"I decrease the quantity of an item in the cart")]
        public void WhenIDecreaseTheQuantityOfAnItemInTheCart()
        {
            updateTableFields();
            updateTableFieldValues();
            IWebElement decreaseButton = rows[0].FindElement(By.XPath("//*[@id=\"Decrease_Button\"]"));
            Thread.Sleep(1000);
            decreaseButton.Click();
        }

        [When(@"I click on the Shop More button")]
        public void WhenIClickOnTheShopMoreButton()
        {
            IWebElement shopMoreButton = _driver.FindElement(By.XPath("//*[@id=\"Shop_More_Button\"]"));
            Thread.Sleep(1000);

            //((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].scrollIntoView(true);", shopMoreButton);

            ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].click();", shopMoreButton);

            //shopMoreButton.Click();
        }

        [Then(@"I should be redirected to the homepage")]
        public void ThenIShouldBeRedirectedToTheHomepage()
        {
            Thread.Sleep(1000);
            Assert.AreEqual("https://localhost:7013/", _driver.Url);
        }

        [When(@"I click on the Checkout button")]
        public void WhenIClickOnTheCheckoutButton()
        {
            IWebElement checkoutButton = _driver.FindElement(By.XPath("//*[@id=\"Checkout_Button\"]"));
            Thread.Sleep(1000);
            ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].click();", checkoutButton);
            //checkoutButton.Click();
        }

        [Then(@"I should be redirected to the checkout page")]
        public void ThenIShouldBeRedirectedToTheCheckoutPage()
        {
            Thread.Sleep(1000);
            Assert.AreEqual("https://localhost:7013/Cart/Checkout", _driver.Url);
        }

        [When(@"I click on the Empty Cart button")]
        public void WhenIClickOnTheEmptyCartButton()
        {
            IWebElement emptyCartButton = _driver.FindElement(By.XPath("//*[@id=\"Clear_Cart_Button\"]"));
            Thread.Sleep(1000);
            ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].click();", emptyCartButton);
            //emptyCartButton.Click();
        }

        [Then(@"Cart empty message should be displayed")]
        public void ThenCartEmptyMessageShouldBeDisplayed()
        {     
            IWebElement emptyCartMessage = _driver.FindElement(By.XPath("//*[@id=\"Cart_Empty_Message\"]"));
            Thread.Sleep(1000);
            Assert.IsTrue(emptyCartMessage.Displayed);
            Assert.IsTrue(emptyCartMessage.Text.Contains("Your shopping cart is empty."));
        }

        public void updateTableFields()
        {
            tableBody = _driver.FindElement(By.XPath("//*[@id=\"Cart_Items_Body\"]"));
            rows = tableBody.FindElements(By.TagName("tr"));
            quantityCell = rows[0].FindElement(By.XPath("//*[@id=\"Cart_Item_Quantity\"]"));
        }

        public void updateTableFieldValues()
        {
            quantity = int.Parse(quantityCell.Text);
        }
    }
}
