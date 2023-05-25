using CoopeguanacasteTiendaWeb.Paginas;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;

namespace CoopeguanacasteTiendaWeb
{
    [TestClass]
    public class CooopeguanacasteTiendaPruebas : DriverHelper
    {
        private PaginaBase paginaBase;
        private PaginaLogin login;
        protected String usuario;
        protected String contrasenna;

        [TestInitialize]
        public void Inicializador()
        {
            usuario = "504010486";
            contrasenna = "Cg12.mm94";

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
            login.IngresarEmailyContrasenna(usuario, contrasenna);
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
            login.IngresarEmailyContrasenna(usuario, contrasenna);

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

            new SelectElement(driver.FindElement(By.Id("instant-search-categories"))).SelectByText("Tecnolog�a");
            driver.FindElement(By.Id("small-searchterms")).Click();
            driver.FindElement(By.Id("small-searchterms")).Clear();
            driver.FindElement(By.Id("small-searchterms")).SendKeys("Celulares");
            Thread.Sleep(2);  
            
            driver.FindElement(By.XPath("//button[@type='submit']")).Click();

            paginaBase.EsperaTituloContenga("B�squeda. Tienda Coopeguanacaste");

            //Assert
            Assert.AreEqual("B�squeda. Tienda Coopeguanacaste", paginaBase.ObtenerTituloPagina());
        }

        [TestMethod]
        public void AgregarProductosAListaDeDeseos()
        {
            //Arrange
            login.IngresarEmailyContrasenna(usuario, contrasenna);
            
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
                dropdown.FindElement(By.XPath("//option[. = 'Persona F�sica Costarricense']")).Click();
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
            driver.FindElement(By.Id("Password")).SendKeys(contrasenna);
            Thread.Sleep(2);   
            driver.FindElement(By.Id("ConfirmPassword")).Click();
            driver.FindElement(By.Id("ConfirmPassword")).SendKeys(contrasenna);
            Thread.Sleep(2);   
            driver.FindElement(By.CssSelector(".k-dropdown-wrap > .k-input")).Click();
            driver.FindElement(By.CssSelector(".k-state-hover")).Click();
            Thread.Sleep(2);
            driver.FindElement(By.Id("StreetAddress")).Click();
            driver.FindElement(By.Id("StreetAddress")).SendKeys("Barrio San Isidro frente al parque los Mara�ones");
            Thread.Sleep(2);   
            driver.FindElement(By.Id("register-button")).Click();
            Thread.Sleep(2);
            paginaBase.EsperaElementoVisible(By.XPath("//*[contains(@id,'dialog-notifications-error')]"));

            //Assert
            String mensajeError = driver.FindElement(By.XPath("//*[contains(@id,'dialog-notifications-error')]")).Text;

            Assert.AreEqual("Ya existe un cliente registrado para este n�mero de c�dula", mensajeError);
        }

        [TestMethod]
        public void IngresoACooopeguanacasteTienda()
        {
            //Arrange
            login.IngresarEmailyContrasenna(usuario, contrasenna);
            //ACT
            Thread.Sleep(5);
            driver.FindElement(By.XPath("//div[@id='header-links-opener']/span")).Click();
            paginaBase.EsperaASerCliclado(By.CssSelector(".ico-account"));
            driver.FindElement(By.CssSelector(".ico-account")).Click();
            Thread.Sleep(2);
            driver.FindElement(By.XPath("//div[@id='header-links-opener']/span")).Click();
            paginaBase.EsperaASerCliclado(By.XPath("//a[contains(text(),'Cerrar sesi�n')]"));
            driver.FindElement(By.XPath("//a[contains(text(),'Cerrar sesi�n')]")).Click();
            paginaBase.EsperaTituloContenga("Tienda Coopeguanacaste");
            Thread.Sleep(2);

            //Assert
            Assert.AreEqual("Tienda Coopeguanacaste", paginaBase.ObtenerTituloPagina());
        }

        [TestMethod]
        public void VerificarOpcionContactenos()
        {
            //Act
            driver.FindElement(By.CssSelector(".mega-menu > li:nth-child(4) span")).Click();
            paginaBase.EsperaTituloContenga("Cont�ctenos. Tienda Coopeguanacaste");
            Thread.Sleep(2);
            driver.FindElement(By.Id("FullName")).Click();
            driver.FindElement(By.Id("FullName")).SendKeys("Nombre");
            Thread.Sleep(2);
            driver.FindElement(By.Id("Email")).Click();
            driver.FindElement(By.Id("Email")).SendKeys("correo01@gmail.com");
            Thread.Sleep(2);
            driver.FindElement(By.Id("Enquiry")).Click();
            driver.FindElement(By.Id("Enquiry")).SendKeys("Consulta sobre art�culo de l�nea blanca");
            Thread.Sleep(2);
            //Se comenta para no enviar mensajes inecesarios
            //driver.FindElement(By.Name("send-email")).Click();

            //Assert
            Assert.AreEqual("Cont�ctenos. Tienda Coopeguanacaste", paginaBase.ObtenerTituloPagina());
        }

        [TestMethod]
        public void VerificarProductosInnovadores()
        {
            //Act
            IWebElement subcatProducto = driver.FindElement(By.CssSelector(".with-dropdown-in-grid > .with-subcategories"));
            Actions builder = new Actions(driver);
            builder.MoveToElement(subcatProducto).Perform();
            Thread.Sleep(2);
            driver.FindElement(By.CssSelector(".hover > span")).Click();
            Thread.Sleep(2);
            paginaBase.EsperaTituloContenga("Innovadores. Tienda Coopeguanacaste");

            //Assert
            Assert.AreEqual("Innovadores. Tienda Coopeguanacaste", paginaBase.ObtenerTituloPagina());
        }

        [TestMethod]
        public void VerificarProductosTecnologicos()
        {
            //Act
            IWebElement subcatProducto = driver.FindElement(By.CssSelector(".with-dropdown-in-grid > .with-subcategories"));
            Actions builder = new Actions(driver);
            builder.MoveToElement(subcatProducto).Perform();
            Thread.Sleep(2);
            driver.FindElement(By.XPath("//img[@alt='Imagen para la categor�a Tecnolog�a']")).Click();
            Thread.Sleep(2);
            paginaBase.EsperaTituloContenga("Tecnolog�a. Tienda Coopeguanacaste");

            //Assert
            Assert.AreEqual("Tecnolog�a. Tienda Coopeguanacaste", paginaBase.ObtenerTituloPagina());
        }


        [TestMethod]
        public void VerificarProductosTV_Audio()
        {
            //Act
            IWebElement subcatProducto = driver.FindElement(By.CssSelector(".with-dropdown-in-grid > .with-subcategories"));
            Actions builder = new Actions(driver);
            builder.MoveToElement(subcatProducto).Perform();
            Thread.Sleep(2);
            driver.FindElement(By.XPath("//img[@alt='Imagen para la categor�a TV & Audio']")).Click();
            Thread.Sleep(2);
            paginaBase.EsperaTituloContenga("TV & Audio. Tienda Coopeguanacaste");

            //Assert
            Assert.AreEqual("TV & Audio. Tienda Coopeguanacaste", paginaBase.ObtenerTituloPagina());
        }

        [TestMethod]
        public void VerificarProductosLineaBlanca()
        {
            //Act
            IWebElement subcatProducto = driver.FindElement(By.CssSelector(".with-dropdown-in-grid > .with-subcategories"));
            Actions builder = new Actions(driver);
            builder.MoveToElement(subcatProducto).Perform();
            Thread.Sleep(2);
            driver.FindElement(By.XPath("//img[@alt='Imagen para la categor�a L�nea Blanca']")).Click();
            Thread.Sleep(2);
            paginaBase.EsperaTituloContenga("L�nea Blanca. Tienda Coopeguanacaste");

            //Assert
            Assert.AreEqual("L�nea Blanca. Tienda Coopeguanacaste", paginaBase.ObtenerTituloPagina());
        }

        [TestMethod]
        public void VerificarProductosPequenosElectrodomesticos()
        {
            //Act
            IWebElement subcatProducto = driver.FindElement(By.CssSelector(".with-dropdown-in-grid > .with-subcategories"));
            Actions builder = new Actions(driver);
            builder.MoveToElement(subcatProducto).Perform();
            Thread.Sleep(2);
            driver.FindElement(By.XPath("//img[@alt='Imagen para la categor�a Peque�os electrodom�sticos']")).Click();
            Thread.Sleep(2);
            paginaBase.EsperaTituloContenga("Peque�os electrodom�sticos. Tienda Coopeguanacaste");

            //Assert
            Assert.AreEqual("Peque�os electrodom�sticos. Tienda Coopeguanacaste", paginaBase.ObtenerTituloPagina());
        }

        [TestMethod]
        public void VerificarProductosEquipoAgricola()
        {
            //Act
            IWebElement subcatProducto = driver.FindElement(By.CssSelector(".with-dropdown-in-grid > .with-subcategories"));
            Actions builder = new Actions(driver);
            builder.MoveToElement(subcatProducto).Perform();
            Thread.Sleep(2);
            IWebElement imgEquipoAgricola = driver.FindElement(By.XPath("//img[@alt='Imagen para la categor�a Equipo Agr�cola']"));
            Actions actions = new Actions(driver);
            actions.MoveToElement(imgEquipoAgricola);
            actions.Perform();
            driver.FindElement(By.XPath("//img[@alt='Imagen para la categor�a Equipo Agr�cola']")).Click();
            Thread.Sleep(2);
            paginaBase.EsperaTituloContenga("Equipo Agr�cola. Tienda Coopeguanacaste");

            //Assert
            Assert.AreEqual("Equipo Agr�cola. Tienda Coopeguanacaste", paginaBase.ObtenerTituloPagina());
        }

        [TestMethod]
        public void VerificarProductosHerramientasElectricas()
        {
            //Act
            IWebElement subcatProducto = driver.FindElement(By.CssSelector(".with-dropdown-in-grid > .with-subcategories"));
            Actions builder = new Actions(driver);
            builder.MoveToElement(subcatProducto).Perform();
            Thread.Sleep(2);
            IWebElement imgHerramientasElectricas = driver.FindElement(By.XPath("//img[@alt='Imagen para la categor�a Herramientas El�ctricas']"));
            Actions actions = new Actions(driver);
            actions.MoveToElement(imgHerramientasElectricas);
            actions.Perform();
            Thread.Sleep(1);
            driver.FindElement(By.XPath("//img[@alt='Imagen para la categor�a Herramientas El�ctricas']")).Click();
            Thread.Sleep(2);
            paginaBase.EsperaTituloContenga("Herramientas El�ctricas. Tienda Coopeguanacaste");

            //Assert
            Assert.AreEqual("Herramientas El�ctricas. Tienda Coopeguanacaste", paginaBase.ObtenerTituloPagina());
        }

        [TestMethod]
        public void VerificarProductosAiresAcondicionados()
        {
            //Act
            IWebElement subcatProducto = driver.FindElement(By.CssSelector(".with-dropdown-in-grid > .with-subcategories"));
            Actions builder = new Actions(driver);
            builder.MoveToElement(subcatProducto).Perform();
            Thread.Sleep(2);
            IWebElement imgAiresAcondicionados = driver.FindElement(By.XPath("//img[@alt='Imagen para la categor�a Aires Acondicionados']"));
            Actions actions = new Actions(driver);
            actions.MoveToElement(imgAiresAcondicionados);
            actions.Perform();
            Thread.Sleep(1);
            driver.FindElement(By.XPath("//img[@alt='Imagen para la categor�a Aires Acondicionados']")).Click();
            Thread.Sleep(2);
            paginaBase.EsperaTituloContenga("Aires Acondicionados. Tienda Coopeguanacaste");

            //Assert
            Assert.AreEqual("Aires Acondicionados. Tienda Coopeguanacaste", paginaBase.ObtenerTituloPagina());
        }

        [TestMethod]
        public void VerificarProductosMaterialesElectricos()
        {
            //Act
            IWebElement subcatProducto = driver.FindElement(By.CssSelector(".with-dropdown-in-grid > .with-subcategories"));
            Actions builder = new Actions(driver);
            builder.MoveToElement(subcatProducto).Perform();
            Thread.Sleep(2);
            IWebElement imgMaterialesElectricos = driver.FindElement(By.XPath("//img[@alt='Imagen para la categor�a Materiales El�ctricos']"));
            Actions actions = new Actions(driver);
            actions.MoveToElement(imgMaterialesElectricos);
            actions.Perform();
            Thread.Sleep(1);
            driver.FindElement(By.XPath("//img[@alt='Imagen para la categor�a Materiales El�ctricos']")).Click();
            Thread.Sleep(2);
            paginaBase.EsperaTituloContenga("Materiales El�ctricos. Tienda Coopeguanacaste");

            //Assert
            Assert.AreEqual("Materiales El�ctricos. Tienda Coopeguanacaste", paginaBase.ObtenerTituloPagina());
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
