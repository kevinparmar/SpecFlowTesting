using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoDi;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TechTalk.SpecFlow;

namespace SpecFlowProjectPizzaShop.Hooks
{
    [Binding]
    public class Hooks
    {
        private IWebDriver driver;
        private readonly IObjectContainer _container;

        public Hooks(IObjectContainer container)
        {
            _container = container;
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            driver = new ChromeDriver();
            
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.Manage().Window.Maximize();
            
            _container.RegisterInstanceAs<IWebDriver>(driver);
        }

        [BeforeScenario("loginRequired")]
        public void BeforeAddToCartLoginScenario()
        {
            driver.Navigate().GoToUrl("https://localhost:7013/Identity/Account/Login");
            IWebElement emailInput = driver.FindElement(By.XPath("//*[@id=\"Input_Email\"]"));
            IWebElement passwordInput = driver.FindElement(By.XPath("//*[@id=\"Input_Password\"]"));
            emailInput.Clear();
            emailInput.SendKeys("user@mail.com");
            passwordInput.Clear();
            passwordInput.SendKeys("Pa$$w0rd");
            IWebElement loginButton = driver.FindElement(By.XPath("//*[@id=\"login-submit\"]"));
            loginButton.Click();
        }

        [BeforeScenario("cartItemsNeeded")]
        public void BeforeCartManagementScenario()
        {
            driver.Navigate().GoToUrl("https://localhost:7013/");

            // Find the pizza container
            IWebElement pizzaContainer = driver.FindElement(By.XPath("//*[@id=\"Pizza_Container\"]"));

            // Find the first two pizza cards
            IList<IWebElement> pizzaCards = pizzaContainer.FindElements(By.XPath("//*[@id=\"Pizza_Card\"]"));

            IWebElement firstPizzaCard = pizzaCards[0];
            IWebElement addToCartButton1 = firstPizzaCard.FindElement(By.XPath(".//*[@id=\"AddToCart\"]"));
            addToCartButton1.Click();

            IWebElement secondPizzaCard = pizzaCards[1];
            IWebElement addToCartButton2 = secondPizzaCard.FindElement(By.XPath(".//*[@id=\"AddToCart\"]"));
            addToCartButton2.Click();
        }


        [AfterScenario]
        public void AfterScenario()
        {
            driver.Quit();
        }
    }
}
