using CoopeguanacasteTiendaWeb.Paginas;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;

namespace CoopeguanacasteTiendaWeb
{
    [TestClass]
    public class ComprasTiendaPruebas : DriverHelper
    {
        private PaginaBase paginaBase;
        private PaginaLogin login;

        [TestInitialize]
        public void Inicializador()
        {
            ChromeOptions opcionesDeChrome = new ChromeOptions
            {
                PageLoadStrategy = PageLoadStrategy.Normal
            };
            opcionesDeChrome.AddArgument(@"--incognito");
            driver = new ChromeDriver(opcionesDeChrome);

            paginaBase = new PaginaBase(driver);
            paginaBase.IrALaPagina();

            login = new PaginaLogin(driver);
        }

        [TestMethod]
        public void VerificarTituloPagina()
        {
            Thread.Sleep(2);   
            Assert.AreEqual("Tienda Coopeguanacaste", paginaBase.ObtenerTituloPagina());
        }

        [TestMethod]
        public void VerProductosEnElCarrito()
        {
            //Arrange
            login.IngresarEmailyContrasenna("504010486", "Cg12.mm94");
            Thread.Sleep(2);

            //Act
            driver.FindElement(By.XPath("//a[contains(text(),'Carrito de compras')]")).Click();
            Thread.Sleep(2);
            paginaBase.EsperaASerCliclado(By.CssSelector("button.button-1.cart-button > span"));
            driver.FindElement(By.CssSelector("button.button-1.cart-button > span")).Click();
            driver.FindElement(By.XPath("//form[@id='shopping-cart-form']/div[2]/div/button")).Click();
            Thread.Sleep(2);

            //Assert
            Assert.AreEqual("Carrito de compras. Tienda Coopeguanacaste", paginaBase.ObtenerTituloPagina());
        }

        [TestMethod]
        public void VerListaDeDeseos()
        {
            //Arrange
            login.IngresarEmailyContrasenna("504010486", "Cg12.mm94");

            Thread.Sleep(2);   

            driver.FindElement(By.CssSelector("div.wishlist-button > a.ico-wishlist")).Click();

            Assert.AreEqual("Lista de Deseos. Tienda Coopeguanacaste", paginaBase.ObtenerTituloPagina());
        }

        [TestMethod]
        public void BusquedaPorCategoria()
        {
            //Arrange
            Thread.Sleep(2);
            
            //Act
            driver.FindElement(By.Id("instant-search-categories")).Click();

            new SelectElement(driver.FindElement(By.Id("instant-search-categories"))).SelectByText("Tecnología");
            driver.FindElement(By.Id("small-searchterms")).Click();
            driver.FindElement(By.Id("small-searchterms")).Clear();
            driver.FindElement(By.Id("small-searchterms")).SendKeys("Celulares");
            Thread.Sleep(2);  
            
            driver.FindElement(By.XPath("//button[@type='submit']")).Click();

            paginaBase.EsperaTituloContenga("Búsqueda. Tienda Coopeguanacaste");

            //Assert
            Assert.AreEqual("Búsqueda. Tienda Coopeguanacaste", paginaBase.ObtenerTituloPagina());
        }

        [TestMethod]
        public void AgregarProductosAListaDeDeseos()
        {
            //Arrange
            login.IngresarEmailyContrasenna("504010486", "Cg12.mm94");
            
            Thread.Sleep(2);

            //Act
            paginaBase.EsperaASerCliclado(By.CssSelector("div.wishlist-button > a.ico-wishlist"));

            driver.FindElement(By.CssSelector("div.wishlist-button > a.ico-wishlist")).Click();

            //Assert
            Assert.AreEqual("Lista de Deseos. Tienda Coopeguanacaste", paginaBase.ObtenerTituloPagina());
        }

        [TestMethod]
        public void RegistrarClienteExistente()
        {
            //Arrange
            driver.FindElement(By.CssSelector("#header-links-opener > span")).Click();
            driver.FindElement(By.CssSelector(".ico-register")).Click();
            driver.FindElement(By.Id("IDType")).Click();
            {
                IWebElement dropdown = driver.FindElement(By.Id("IDType"));
                dropdown.FindElement(By.XPath("//option[. = 'Persona Física Costarricense']")).Click();
            }
            driver.FindElement(By.Id("IDNumber")).Click();
            driver.FindElement(By.Id("IDNumber")).SendKeys("5-04010486");
            Thread.Sleep(2);   
            driver.FindElement(By.CssSelector(".male > .forcheckbox")).Click();
            driver.FindElement(By.Id("FirstName")).Click();
            Thread.Sleep(2);   
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
            Thread.Sleep(2);   
            driver.FindElement(By.Id("Email")).Click();
            driver.FindElement(By.Id("Email")).SendKeys("christiangeo94@hotmail.com");
            driver.FindElement(By.Id("Password")).Click();
            driver.FindElement(By.Id("Password")).SendKeys("Cg12.mm94");
            Thread.Sleep(2);   
            driver.FindElement(By.Id("ConfirmPassword")).Click();
            driver.FindElement(By.Id("ConfirmPassword")).SendKeys("Cg12.mm94");
            Thread.Sleep(2);   
            driver.FindElement(By.CssSelector(".k-dropdown-wrap > .k-input")).Click();
            driver.FindElement(By.CssSelector(".k-state-hover")).Click();
            Thread.Sleep(2);
            driver.FindElement(By.Id("StreetAddress")).Click();
            driver.FindElement(By.Id("StreetAddress")).SendKeys("Barrio San Isidro frente al parque los Marañones");
            Thread.Sleep(2);   
            driver.FindElement(By.Id("register-button")).Click();
            Thread.Sleep(2);
            paginaBase.EsperaElementoVisible(By.XPath("//*[contains(@id,'dialog-notifications-error')]"));

            //Assert
            String mensajeError = driver.FindElement(By.XPath("//*[contains(@id,'dialog-notifications-error')]")).Text;

            Assert.AreEqual("Ya existe un cliente registrado para este número de cédula", mensajeError);
        }

        [TestMethod]
        public void IngresoACooopeguanacasteTienda()
        {
            //Arrange
            login.IngresarEmailyContrasenna("504010486", "Cg12.mm94");

            //ACT
            Thread.Sleep(2);
            driver.FindElement(By.CssSelector("#header-links-opener > span")).Click();
            paginaBase.EsperaElementoVisible(By.XPath("//a[contains(text(),'Mi cuenta')]"));
            driver.FindElement(By.XPath("//a[contains(text(),'Mi cuenta')]")).Click();
            Thread.Sleep(2);
            driver.FindElement(By.CssSelector("#header-links-opener > span")).Click();
            driver.FindElement(By.XPath("//a[contains(text(),\'Cerrar sesión\')]")).Click();
            paginaBase.EsperaTituloContenga("Tienda Coopeguanacaste");
            Thread.Sleep(2);

            //Assert
            Assert.AreEqual("Tienda Coopeguanacaste", paginaBase.ObtenerTituloPagina());
        }

        [TestCleanup]
        public void LimpiarClase()
        {
            try
            {
                paginaBase.LimpiarPruebas();
            }
            catch (Exception)
            {
            }
        }
    }
}
