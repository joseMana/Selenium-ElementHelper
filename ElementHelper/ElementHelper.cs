using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using OpenQA.Selenium;

namespace ElementHelpr

{
    public enum ElementType
    {
        Name,
        ClassName,
        Id,
        TagName,
    }
    public class ElementHelper
    {
        public IWebDriver driver;
        public IWebElement elementContainer;
        public IJavaScriptExecutor js;

        public ElementHelper(IWebDriver driver, IWebElement elementContainer)
        {
            this.driver = driver;
            this.elementContainer = elementContainer;
            this.js = ((IJavaScriptExecutor)driver);
        }

        public void WaitForElement(string elementToWait, ElementType by)
        {
            while (true)
            {

                if (IsElementLoaded(elementToWait, by))
                {
                    Thread.Sleep(2000);
                    continue;
                }
                else
                {
                    break;
                }
            }
        }
        public bool IsElementLoaded(string elementToWait, ElementType by)
        {

            try
            {
                elementContainer = driver.FindElement(getByObject(by, elementToWait));
                return false;
            }
            catch
            {
                return true;
            }
        }
        public By getByObject(ElementType by, string elementToWait)
        {
            var objBy = By.Name("temp");
            switch (by)
            {
                case ElementType.Name:
                    objBy = By.Name(elementToWait);
                    break;

                case ElementType.ClassName:
                    objBy = By.ClassName(elementToWait);
                    break;

                case ElementType.Id:
                    objBy = By.Id(elementToWait);
                    break;

                case ElementType.TagName:
                    objBy = By.TagName(elementToWait);
                    break;
            }
            return objBy;
        }

        public void JsExecute(string jsMethod)
        {
            int counter = 0;
            while (counter < 10)
            {
                Thread.Sleep(1000);
                try
                {
                    js.ExecuteScript(jsMethod);
                    break;
                }
                catch
                {
                    counter++;
                    continue;
                }
            }
        }

        public void SeleniumJsExecute(string jsMethod, ElementType by)
        {
            while (true)
            {
                Thread.Sleep(1000);
                try
                {
                    switch (by)
                    {
                        case ElementType.Name:
                            driver.FindElement(By.Name(jsMethod)).Click();
                            break;
                        case ElementType.Id:
                            driver.FindElement(By.Id(jsMethod)).Click();
                            break;
                    }

                    break;
                }
                catch
                {
                    continue;
                }
            }
        }
    }
}

