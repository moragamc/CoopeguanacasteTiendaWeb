using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Linq;

namespace CoopeguanacasteTiendaWeb
{
    [TestClass]
    public class ComprasTiendaPruebas
    {

        private ChromeDriver driver;
        private static string baseURL;
        private WebDriverWait espera;
        //private String currentURL;

        [TestInitialize]
        public void ChromeDriverInitialize()
        {
            // Initialize
            ChromeOptions options = new ChromeOptions
            {
                PageLoadStrategy = PageLoadStrategy.Normal              
            };
            options.AddArgument(@"--incognito");
            driver = new ChromeDriver(options);
            baseURL = "https://tienda.coopeguanacaste.com/";
            driver.Url = baseURL;
            espera = new WebDriverWait(driver, new TimeSpan(0, 0, 30));
            driver.Manage().Window.Maximize();

        }
        public void InicioSesion()
        {
            driver.FindElement(By.XPath("//div[@id='header-links-opener']/span")).Click();
            driver.Navigate().GoToUrl($"{baseURL}extend/login");
            //TimeSpan.FromSeconds(2);
            driver.FindElement(By.Id("Username")).Click();
            driver.FindElement(By.Id("Username")).Clear();
            driver.FindElement(By.Id("Username")).SendKeys("504010486");
            //TimeSpan.FromSeconds(2);
            driver.FindElement(By.Id("Password")).Click();
            driver.FindElement(By.Id("Password")).Clear();
            driver.FindElement(By.Id("Password")).SendKeys("Cg12.mm94");
            //TimeSpan.FromSeconds(2);
            driver.FindElement(By.CssSelector(".login-button")).Click();
            TimeSpan.FromSeconds(2);
            driver.Navigate().GoToUrl(baseURL);
            espera.Until(ExpectedConditions.TitleContains("Tienda Coopeguanacaste"));
        }

        public void CerrarSesion()
        {
            driver.FindElement(By.XPath("//div[@id='header-links-opener']/span")).Click();
            TimeSpan.FromSeconds(2);
            driver.FindElement(By.XPath("//a[contains(text(),'Cerrar sesión')]")).Click();
            TimeSpan.FromSeconds(2);
            driver.Navigate().GoToUrl(baseURL);
        }

        [TestMethod]
        public void VerificarTituloPagina()
        {
            TimeSpan.FromSeconds(2);
            Assert.AreEqual("Tienda Coopeguanacaste", driver.Title);
        }

        [TestMethod]
        public void VerProductosEnElCarrito()
        {
            InicioSesion();
            /*
            driver.FindElement(By.XPath("//div[@id='header-links-opener']/span")).Click();
            driver.Navigate().GoToUrl($"{baseURL}extend/login");
            //TimeSpan.FromSeconds(2);
            driver.FindElement(By.Id("Username")).Click();
            driver.FindElement(By.Id("Username")).Clear();
            driver.FindElement(By.Id("Username")).SendKeys("504010486");
            //TimeSpan.FromSeconds(2);
            driver.FindElement(By.Id("Password")).Click();
            driver.FindElement(By.Id("Password")).Clear();
            driver.FindElement(By.Id("Password")).SendKeys("Cg12.mm94");
            //TimeSpan.FromSeconds(2);
            driver.FindElement(By.CssSelector(".login-button")).Click();
            driver.Navigate().GoToUrl(baseURL);
            */
            TimeSpan.FromSeconds(2);
            driver.FindElement(By.XPath("//a[contains(text(),'Carrito de compras')]")).Click();
            TimeSpan.FromSeconds(2);
            espera.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("button.button-1.cart-button > span")));
            driver.FindElement(By.CssSelector("button.button-1.cart-button > span")).Click();
            driver.FindElement(By.XPath("//form[@id='shopping-cart-form']/div[2]/div/button")).Click();
            TimeSpan.FromSeconds(2);
            Assert.AreEqual("Carrito de compras. Tienda Coopeguanacaste", driver.Title);
            //CerrarSesion();
        }

        [TestMethod]
        public void VerListaDeDeseos()
        {
            InicioSesion();

            TimeSpan.FromSeconds(2);

            driver.FindElement(By.CssSelector("div.wishlist-button > a.ico-wishlist")).Click();

            Assert.AreEqual("Lista de Deseos. Tienda Coopeguanacaste", driver.Title);

            //CerrarSesion();
        }

        [TestMethod]
        public void BusquedaPorCategoria()
        {
            TimeSpan.FromSeconds(2);
            driver.FindElement(By.Id("instant-search-categories")).Click();
            new SelectElement(driver.FindElement(By.Id("instant-search-categories"))).SelectByText("Tecnología");
            driver.FindElement(By.Id("small-searchterms")).Click();
            driver.FindElement(By.Id("small-searchterms")).Clear();
            driver.FindElement(By.Id("small-searchterms")).SendKeys("Celulares");
            TimeSpan.FromSeconds(2);
            driver.FindElement(By.XPath("//button[@type='submit']")).Click();

            espera.Until(ExpectedConditions.TitleContains(driver.Title));

            Assert.AreEqual("Búsqueda. Tienda Coopeguanacaste", driver.Title);
        }

        [TestMethod]
        public void AgregarProductosAListaDeDeseos()
        {
            InicioSesion();
            TimeSpan.FromSeconds(3);
            //espera.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//li[2]/span")));
            //driver.FindElement(By.XPath("//li[2]/span")).Click(); 
            //TimeSpan.FromSeconds(2);
            /*
            driver.FindElement(By.CssSelector("a[title='Mostrar productos en la categoría Tecnología'] > img.lazy")).Click();
            TimeSpan.FromSeconds(2);
            driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Añadir a la lista de comparación'])[6]/following::button[1]")).Click();
            TimeSpan.FromSeconds(2);
            driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Cantidad : 1'])[1]/following::button[1]")).Click();
            */
            
            //driver.FindElement(By.XPath("//a[@href='/tecnologia']")).Click();
            //TimeSpan.FromSeconds(2);
            //driver.FindElement(By.XPath("//button[@class='button-2 add-to-wishlist-button']")).Click();
            //TimeSpan.FromSeconds(3);
            //driver.FindElement(By.CssSelector(".item-box:nth-child(6) .add-to-wishlist-button")).Click();
            //TimeSpan.FromSeconds(2);
            //espera.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(".productAddedToCartWindowCheckout")));
            //driver.FindElement(By.CssSelector(".productAddedToCartWindowCheckout")).Click();
            
            TimeSpan.FromSeconds(2);
            //espera.Until(ExpectedConditions.ElementToBeClickable(By.Name("updatecart")));
            //driver.FindElement(By.Name("updatecart")).Click();
            //Assert.AreEqual("Tienda Coopeguanacaste", driver.Title);
            espera.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("div.wishlist-button > a.ico-wishlist")));
            driver.FindElement(By.CssSelector("div.wishlist-button > a.ico-wishlist")).Click();

            Assert.AreEqual("Lista de Deseos. Tienda Coopeguanacaste", driver.Title);
            //CerrarSesion();
        }

        [TestMethod]
        public void RegistrarClienteExistente()
        {
            driver.FindElement(By.CssSelector("#header-links-opener > span")).Click();
            driver.FindElement(By.CssSelector(".ico-register")).Click();
            driver.FindElement(By.Id("IDType")).Click();
            {
                IWebElement dropdown = driver.FindElement(By.Id("IDType"));
                dropdown.FindElement(By.XPath("//option[. = 'Persona Física Costarricense']")).Click();
            }
            driver.FindElement(By.Id("IDNumber")).Click();
            driver.FindElement(By.Id("IDNumber")).SendKeys("5-04010486");
            TimeSpan.FromSeconds(2);
            driver.FindElement(By.CssSelector(".male > .forcheckbox")).Click();
            //driver.FindElement(By.Id("FirstName")).Click();
            TimeSpan.FromSeconds(2);
            driver.FindElement(By.Name("DateOfBirthDay")).Click();
            {
                IWebElement dropdownDay = driver.FindElement(By.Name("DateOfBirthDay"));
                dropdownDay.FindElement(By.XPath("//option[. = '11']")).Click();
            }
            driver.FindElement(By.Name("DateOfBirthMonth")).Click();
            {
                IWebElement dropdownMonth = driver.FindElement(By.Name("DateOfBirthMonth"));
                dropdownMonth.FindElement(By.XPath("//option[. = 'noviembre']")).Click();
            }
            driver.FindElement(By.Name("DateOfBirthYear")).Click();
            {
                IWebElement dropdownYear = driver.FindElement(By.Name("DateOfBirthYear"));
                dropdownYear.FindElement(By.XPath("//option[. = '1994']")).Click();
            }
            TimeSpan.FromSeconds(2);
            driver.FindElement(By.Id("Email")).Click();
            driver.FindElement(By.Id("Email")).SendKeys("christiangeo94@hotmail.com");
            driver.FindElement(By.Id("Password")).Click();
            driver.FindElement(By.Id("Password")).SendKeys("Cg12.mm94");
            TimeSpan.FromSeconds(2);
            driver.FindElement(By.Id("ConfirmPassword")).Click();
            driver.FindElement(By.Id("ConfirmPassword")).SendKeys("Cg12.mm94");
            TimeSpan.FromSeconds(2);
            driver.FindElement(By.CssSelector(".k-dropdown-wrap > .k-input")).Click();
            driver.FindElement(By.CssSelector(".k-state-hover")).Click();
            TimeSpan.FromSeconds(2);
            //driver.FindElement(By.Id("StreetAddress")).Click();
            //driver.FindElement(By.Id("StreetAddress")).SendKeys("Barrio San Isidro frente al parque los Marañones");
            TimeSpan.FromSeconds(2);
            //driver.FindElement(By.Id("register-button")).Click();
            TimeSpan.FromSeconds(2);
            espera.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[contains(@id,'dialog-notifications-error')]")));
            String mensajeError = driver.FindElement(By.XPath("//*[contains(@id,'dialog-notifications-error')]")).Text;

            Assert.AreEqual("Ya existe un cliente registrado para este número de cédula", mensajeError);
        }

        [TestMethod]
        public void IngresoACooopeguanacasteTienda()
        {
            driver.FindElement(By.XPath("//div[@id='header-links-opener']/span")).Click();
            driver.Navigate().GoToUrl($"{baseURL}extend/login");
            //TimeSpan.FromSeconds(2);
            //driver.FindElement(By.Id("Username")).Click();
            driver.FindElement(By.Id("Username")).Clear();
            driver.FindElement(By.Id("Username")).SendKeys("504010486");
            TimeSpan.FromSeconds(2);
            //driver.FindElement(By.Id("Password")).Click();
            driver.FindElement(By.Id("Password")).Clear();
            driver.FindElement(By.Id("Password")).SendKeys("Cg12.mm94");
            //TimeSpan.FromSeconds(2);
            driver.FindElement(By.CssSelector(".login-button")).Click();
            TimeSpan.FromSeconds(4);
            driver.Navigate().GoToUrl(baseURL);
            espera.Until(ExpectedConditions.TitleContains("Tienda Coopeguanacaste"));
            TimeSpan.FromSeconds(5);

            Assert.AreEqual("Tienda Coopeguanacaste", driver.Title);
            /*
            driver.FindElement(By.XPath("//div[@id='header-links-opener']/span")).Click();
            driver.FindElement(By.CssSelector(".ico-account")).Click();
            var newWindow = driver.SwitchTo().Window(newWindowHandle);
            newWindow.WindowHandles.FirstOrDefault();
            newWindow.Navigate().GoToUrl($"{baseURL}Plugins/EcommerceCostaRica/CustomerExtend/Info");
            TimeSpan.FromSeconds(2);
            newWindow.FindElement(By.CssSelector("#header-links-opener > span")).Click();
            newWindow.FindElement(By.CssSelector(".ico-logout")).Click();
            */
            //driver.FindElement(By.CssSelector("#header-links-opener > span")).Click();
            //TimeSpan.FromSeconds(2);
            //var newWindow = driver.SwitchTo().Window(newWindowHandle);
            //newWindow.WindowHandles.FirstOrDefault();
            //driver.FindElement(By.CssSelector(".ico-account")).Click();
            //driver.FindElement(By.CssSelector("#header-links-opener > span")).Click();
            //driver.FindElement(By.CssSelector(".ico-logout")).Click();


            //espera.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//a[contains(text(),'Mi cuenta')]")));
            //driver.FindElement(By.XPath("//a[contains(text(),'Mi cuenta')]")).Click();
            //TimeSpan.FromSeconds(2);
            ////driver.Navigate().GoToUrl($"{baseURL}Plugins/EcommerceCostaRica/CustomerExtend/Info");
            //driver.FindElement(By.Id("save-info-button")).Click();
            //driver.FindElement(By.XPath("//div[@id='header-links-opener']/span")).Click();
            //driver.FindElement(By.CssSelector(".ico-logout")).Click();

            //CerrarSesion();
        }

        [TestCleanup]
        public void CleanupClass()
        {
            try
            {
                //driver.Quit();// quit does not close the window
                driver.Close();
                driver.Dispose();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
        }
    }
}
