# Trabajo-LAB-4

Integrantes: Tobías Fernandez, Bruno Menna, Matias Ruiz

# Resumen

Nuestro trabajo es un una tienda virtual de celulares, en la que manejamos 4 tipos de usuarios: Administradores, SysAdmin, Usuarios y Usuario deshabilitado (bajá lógica de usuarios, se le revocan todos los permisos).<br />
Nuestra web presenta una vista pública de los productos, pero requiere loguearse para realizar una compra y acceder a un historial de estas. <br />
Cuando se eligen los productos se almacenan en un carrito, que al comprar desde este pedirá los datos del usuario para realizar el pedido que comienza como Pendiente. Los administradores pueden actualizar el estado de estos. <br />
Los Administradores pueden agregar nuevos productos, manejar los pedidos de los usuarios y tener 2 resumenes gráficos de las estadisticas de venta. <br />
El sysAdmin puede acceder a una lista de todos los usuarios, cambiar los permisos de los mismos y realizar las actividades de los demás administradores. <br />
Tenemos un apartado de Preguntas Frecuentes, pero también contamos con un soporte via WhatsApp, redirigiendo a los usuarios a este en caso de necesitar aclarar mas dudas. <br />

# Ubicacion de requisitos TPI

# FrontEnd
https://github.com/BrunoCMenna/Trabajo-LAB <br />

Custom Hooks: se encuentran en TopProducts.js (useTopProductsNavigation) y en Cart.js (hooks/useFormattedNumber.js) <br />
Testing Unitario: se le realizó al componente SignIn, la función Validation que valida el estado de los datos antes de entrar y enviar el formulario de registro. <br />
Componente de chat: se encuentra en la carpeta SupportPopup, es un re-direccionado a whatsapp API.<br />
Contamos con modo oscuro en toda la web.<br />

# BackEnd
El BackEnd se encuentra en privado, ya que una API que utilizamos lo requería, ambos profesores fueron invitados a colaboradores.

https://github.com/BrunoCMenna/API-TrabajoPractico-Prog <br />

Contamos con 3 ABM (usuarios, productos, pedidos). <br />
Contamos con permisos sobre los endpoints acorde a los roles.<br />
Tenemos 4 tipo de Roles: Administradores, SysAdmin, Usuarios y Disabled (para baja lógica de usuarios). <br />
Utilizamos JWT para el sistema de registro y login de nuestros usuarios. <br />
Como consulta compleja implementamos GetTopProducts (backend) y la utilizamos en TopProducts (frontend). <br />
La implementacion del patron visto en clase fue Prototype y fue utilizada en un Historial de productos en HistoricProducts. <br />
