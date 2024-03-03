namespace SecHex_GUI.SRC
{
    using System;
    using System.ComponentModel;
    using System.Management;
    using System.Collections;
    using System.Globalization;


    // Las funciones ShouldSerialize<PropertyName> son funciones que utiliza el Examinador de propiedades de VS para comprobar si se tiene que serializar una propiedad determinada. Estas funciones se agregan para todas las propiedades ValueType (propiedades de tipo Int32, BOOL etc. que no se pueden establecer como NULL). Estas funciones utilizan la función Is<PropertyName>Null. Asimismo, se utilizan en la implementación de TypeConverter para que las propiedades comprueben el valor NULL de una propiedad, de modo que se pueda mostrar un valor vacío en el Examinador de propiedades si se utiliza la función de arrastrar y colocar en Visual Studio.
    // Las funciones Is<PropertyName>Null() se utilizan para comprobar si una propiedad tiene valores NULL.
    // Las funciones Reset<PropertyName> se agregan para las propiedades Nullable Read/Write. El diseñador de VS utiliza estas funciones en el Examinador de propiedades para establecer una propiedad como NULL.
    // Todas las propiedades que se agregan a la clase de la propiedad WMI tienen atributos establecidos para definir su comportamiento en el diseñador de Visual Studio, así como para definir el elemento TypeConverter que se debe utilizar.
    // Las funciones Datetime de conversión ToDateTime y ToDmtfDateTime se agregan a la clase para convertir la fecha y hora DMTF a System.DateTime y viceversa.
    // Se generó una clase Early Bound para la clase WMI.Win32_NetworkAdapter
    public class NetworkAdapter : Component
    {

        // Propiedad privada que contiene el espacio de nombres WMI en el que reside la clase.
        private static string CreatedWmiNamespace = "root\\CimV2";

        // Propiedad privada que mantiene el nombre de la clase WMI, que creó esta clase.
        private static string CreatedClassName = "Win32_NetworkAdapter";

        // Variable miembro privada que contiene el valor ManagementScope que utilizan los diferentes métodos.
        private static ManagementScope statMgmtScope = null;

        private ManagementSystemProperties PrivateSystemProperties;

        // Objeto lateBound de WMI subyacente.
        private ManagementObject PrivateLateBoundObject;

        // Variable miembro que almacena el comportamiento de 'confirmación automática' de la clase.
        private bool AutoCommitProp;

        // Variable privada que contiene la propiedad incrustada que representa la instancia.
        private ManagementBaseObject embeddedObj;

        // Objeto WMI actual utilizado
        private ManagementBaseObject curObj;

        // Etiqueta para indicar si la instancia es un objeto incrustado.
        private bool isEmbedded;

        // A continuación se muestran las diferentes sobrecargas de constructores para inicializar una instancia de la clase con un objeto WMI.
        public NetworkAdapter()
        {
            InitializeObject(null, null, null);
        }

        public NetworkAdapter(string keyDeviceID)
        {
            InitializeObject(null, new ManagementPath(ConstructPath(keyDeviceID)), null);
        }

        public NetworkAdapter(ManagementScope mgmtScope, string keyDeviceID)
        {
            InitializeObject(mgmtScope, new ManagementPath(ConstructPath(keyDeviceID)), null);
        }

        public NetworkAdapter(ManagementPath path, ObjectGetOptions getOptions)
        {
            InitializeObject(null, path, getOptions);
        }

        public NetworkAdapter(ManagementScope mgmtScope, ManagementPath path)
        {
            InitializeObject(mgmtScope, path, null);
        }

        public NetworkAdapter(ManagementPath path)
        {
            InitializeObject(null, path, null);
        }

        public NetworkAdapter(ManagementScope mgmtScope, ManagementPath path, ObjectGetOptions getOptions)
        {
            InitializeObject(mgmtScope, path, getOptions);
        }

        public NetworkAdapter(ManagementObject theObject)
        {
            Initialize();
            if (CheckIfProperClass(theObject) == true)
            {
                PrivateLateBoundObject = theObject;
                PrivateSystemProperties = new ManagementSystemProperties(PrivateLateBoundObject);
                curObj = PrivateLateBoundObject;
            }
            else
            {
                throw new ArgumentException("El nombre de clase no coincide.");
            }
        }

        public NetworkAdapter(ManagementBaseObject theObject)
        {
            Initialize();
            if (CheckIfProperClass(theObject) == true)
            {
                embeddedObj = theObject;
                PrivateSystemProperties = new ManagementSystemProperties(theObject);
                curObj = embeddedObj;
                isEmbedded = true;
            }
            else
            {
                throw new ArgumentException("El nombre de clase no coincide.");
            }
        }

        // La propiedad devuelve el espacio de nombres de la clase WMI.
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string OriginatingNamespace
        {
            get
            {
                return "root\\CimV2";
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string ManagementClassName
        {
            get
            {
                string strRet = CreatedClassName;
                if (curObj != null)
                {
                    if (curObj.ClassPath != null)
                    {
                        strRet = (string)curObj["__CLASS"];
                        if (strRet == null
                                    || strRet == string.Empty)
                        {
                            strRet = CreatedClassName;
                        }
                    }
                }
                return strRet;
            }
        }

        // Propiedad que señala a un objeto incrustado para obtener las propiedades System del objeto WMI.
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ManagementSystemProperties SystemProperties
        {
            get
            {
                return PrivateSystemProperties;
            }
        }

        // Propiedad que devuelve el objeto lateBound subyacente.
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ManagementBaseObject LateBoundObject
        {
            get
            {
                return curObj;
            }
        }

        // ManagementScope del objeto.
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ManagementScope Scope
        {
            get
            {
                if (isEmbedded == false)
                {
                    return PrivateLateBoundObject.Scope;
                }
                else
                {
                    return null;
                }
            }
            set
            {
                if (isEmbedded == false)
                {
                    PrivateLateBoundObject.Scope = value;
                }
            }
        }

        // Propiedad que muestra el comportamiento de confirmación del objeto WMI. Si se establece como true, el objeto WMI se guarda automáticamente después de modificar cada propiedad. Por ejemplo: se llama a Put() después de modificar una propiedad.
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool AutoCommit
        {
            get
            {
                return AutoCommitProp;
            }
            set
            {
                AutoCommitProp = value;
            }
        }

        // ManagementPath del objeto WMI subyacente.
        [Browsable(true)]
        public ManagementPath Path
        {
            get
            {
                if (isEmbedded == false)
                {
                    return PrivateLateBoundObject.Path;
                }
                else
                {
                    return null;
                }
            }
            set
            {
                if (isEmbedded == false)
                {
                    if (CheckIfProperClass(null, value, null) != true)
                    {
                        throw new ArgumentException("El nombre de clase no coincide.");
                    }
                    PrivateLateBoundObject.Path = value;
                }
            }
        }

        // Propiedad pública de ámbito estático que utilizan los diferentes métodos.
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public static ManagementScope StaticScope
        {
            get
            {
                return statMgmtScope;
            }
            set
            {
                statMgmtScope = value;
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("La propiedad AdapterType refleja el medio de red que que usa. Es posible que esta" +
            " propiedad no sea aplicable para todos los tipos de adaptadores de red en esta c" +
            "lase. Sólo Windows NT.")]
        public string AdapterType
        {
            get
            {
                return (string)curObj["AdapterType"];
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsAdapterTypeIdNull
        {
            get
            {
                if (curObj["AdapterTypeId"] == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description(@"La propiedad AdapterTypeId refleja el medio de red en uso. Esta propiedad da la misma información que la propiedad AdapterType, excepto que la información se devuelve en la forma de un valor entero que corresponde a lo siguiente: 
0 - Ethernet 802.3
1 - Token Ring 802.5
2 - Fiber Distributed Data Interface (FDDI)
3 - Wide Area Network (WAN)
4 - LocalTalk
5 - Ethernet usando el formato de encabezado DIX
6 - ARCNET
7 - ARCNET (878.2)
8 - ATM
9 - Wireless
10 - Infrared Wireless
11 - Bpc
12 - CoWan
13 - 1394
Esta propiedad puede ser no aplicable a todos los tipos de adaptadores de red listados dentro de esta clase. Sólo Windows NT.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public AdapterTypeIdValues AdapterTypeId
        {
            get
            {
                if (curObj["AdapterTypeId"] == null)
                {
                    return (AdapterTypeIdValues)Convert.ToInt32(14);
                }
                return (AdapterTypeIdValues)Convert.ToInt32(curObj["AdapterTypeId"]);
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsAutoSenseNull
        {
            get
            {
                if (curObj["AutoSense"] == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Un valor booleano que indica si el adaptador de red puede determinar automáticame" +
            "nte la velocidad u otras características de comunicaciones del medio de red adju" +
            "ntado.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public bool AutoSense
        {
            get
            {
                if (curObj["AutoSense"] == null)
                {
                    return Convert.ToBoolean(0);
                }
                return (bool)curObj["AutoSense"];
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsAvailabilityNull
        {
            get
            {
                if (curObj["Availability"] == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description(@"La disponibilidad y estado del dispositivo. Por ejemplo, la propiedad disponibilidad, indica que el dispositivo está en funcionamiento y tiene energía total (valor=3), o se encuentra en un estado de aviso (4), prueba (5), degradado (10) o ahorro de energía (valores 13-15 y 17). En relación con los estados de ahorro de energía, éstos se definen como sigue: Valor 13 (""Ahorro de energía: desconocido"") indica que se sabe que el dispositivo está en un modo de ahorro de energía, pero se desconoce su estado exacto en este modo; 14 (""Ahorro de energía: modo de bajo consumo"") indica que el dispositivo está en un estado de  ahorro de energía, pero sigue funcionando y puede exhibir una baja de rendimiento;  15 (""Ahorro de energía: espera"") describe que el sistema no está en funcionamiento, pero que se podría poner en operación ""rápidamente""; y valor 17 (""Ahorro de energía: advertencia"") indica que el equipo está en un estado de aviso, aunque está también en modo de ahorro de energía.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public AvailabilityValues Availability
        {
            get
            {
                if (curObj["Availability"] == null)
                {
                    return (AvailabilityValues)Convert.ToInt32(0);
                }
                return (AvailabilityValues)Convert.ToInt32(curObj["Availability"]);
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Caption
        {
            get
            {
                return (string)curObj["Caption"];
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsConfigManagerErrorCodeNull
        {
            get
            {
                if (curObj["ConfigManagerErrorCode"] == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Indica el código de error del Administrador de configuración de Win32. Los valore" +
            "s siguientes pueden ser devueltos: \n0 Este dispositivo funciona correctamente. \n" +
            "1 Este dispositivo no está configurado correctamente. \n2 Windows no puede cargar" +
            " el controlador para este dispositivo. \n3 El controlador de este dispositivo pue" +
            "de estar dañado o le falta memoria o recursos a su sistema. \n4 Este dispositivo " +
            "no funciona correctamente. Uno de sus controladores o el Registro pueden estar d" +
            "añados. \n5 El controlador de este dispositivo necesita un recurso que Windows no" +
            " puede administrar. \n6 La configuración de arranque de este dispositivo entra en" +
            " conflicto con otros dispositivos. \n7 No se puede filtrar. \n8 Falta el cargador " +
            "de controlador del dispositivo. \n9 Este dispositivo no funciona correctamente po" +
            "rque el firmware de control está informando incorrectamente acerca de los recurs" +
            "os del dispositivo. \n10 El dispositivo no puede se iniciar. \n11 Error en el disp" +
            "ositivo. \n12 Este dispositivo no encuentra suficientes recursos libres para usar" +
            ". \n13 Windows no puede comprobar los recursos de este dispositivo. \n14 Este disp" +
            "ositivo no funcionará correctamente hasta que reinicie su equipo. \n15 Este dispo" +
            "sitivo no funciona correctamente porque hay un posible problema de enumeración. " +
            "\n16 Windows no puede identificar todos los recursos que utiliza este dispositivo" +
            ". \n17 Este dispositivo está solicitando un tipo de recurso desconocido. \n18 Vuel" +
            "va a instalar los controladores de este dispositivo \n19 Su Registro debe estar d" +
            "añado. \n20 Error usar el cargador VxD. \n21 Error del sistema: intente cambiar el" +
            " controlador de este dispositivo. Si esto no funciona, consulte la documentación" +
            " de hardware. Windows está quitando este dispositivo. \n22 Este dispositivo está " +
            "deshabilitado. \n23 Error del sistema: intente cambiar el controlador de este dis" +
            "positivo. Si esto no funciona, consulte la documentación de hardware. \n24 Este d" +
            "ispositivo no está presente, no funciona correctamente o no tiene todos los cont" +
            "roladores instalados. \n25 Windows aún está instalando este dispositivo. \n26 Wind" +
            "ows aún está instalando este dispositivo. \n27 Este dispositivo no tiene una conf" +
            "iguración de Registro válida. \n28 Los controladores de este dispositivo no están" +
            " instalados. \n29 Este dispositivo está deshabilitado porque el firmware no propo" +
            "rcionó los recursos requeridos. \n30 Este dispositivo está utilizando una recurso" +
            " de solicitud de interrupción (IRQ) que ya está usando otro dispositivo. \n31 Est" +
            "e dispositivo no funciona correctamente porque Windows no puede cargar los contr" +
            "oladores requeridos.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public ConfigManagerErrorCodeValues ConfigManagerErrorCode
        {
            get
            {
                if (curObj["ConfigManagerErrorCode"] == null)
                {
                    return (ConfigManagerErrorCodeValues)Convert.ToInt32(32);
                }
                return (ConfigManagerErrorCodeValues)Convert.ToInt32(curObj["ConfigManagerErrorCode"]);
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsConfigManagerUserConfigNull
        {
            get
            {
                if (curObj["ConfigManagerUserConfig"] == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Indica si el dispositivo usa una configuración predefinida por el usuario.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public bool ConfigManagerUserConfig
        {
            get
            {
                if (curObj["ConfigManagerUserConfig"] == null)
                {
                    return Convert.ToBoolean(0);
                }
                return (bool)curObj["ConfigManagerUserConfig"];
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description(@"CreationClassName indica el nombre de la clase o subclase que se usa en la creación de una instancia. Cuando se usa con las demás propiedades clave de esta clase, esta propiedad permite que se identifiquen de manera única todas las instancias de esta clase y sus subclases.")]
        public string CreationClassName
        {
            get
            {
                return (string)curObj["CreationClassName"];
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Description
        {
            get
            {
                return (string)curObj["Description"];
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("La propiedad DeviceID contiene una cadena que identifica de forma única el adapta" +
            "dor de red con respecto a otros dispositivos del sistema.")]
        public string DeviceID
        {
            get
            {
                return (string)curObj["DeviceID"];
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsErrorClearedNull
        {
            get
            {
                if (curObj["ErrorCleared"] == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("ErrorCleared es una propiedad booleana que indica que el error comunicado en la p" +
            "ropiedad LastErrorCode se ha resuelto ahora.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public bool ErrorCleared
        {
            get
            {
                if (curObj["ErrorCleared"] == null)
                {
                    return Convert.ToBoolean(0);
                }
                return (bool)curObj["ErrorCleared"];
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("ErrorDescription es una cadena de forma libre que ofrece más información acerca d" +
            "el error registrado en la propiedad LastErrorCode e información acerca de cualqu" +
            "ier acción correctiva que se pueda tomar.")]
        public string ErrorDescription
        {
            get
            {
                return (string)curObj["ErrorDescription"];
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("La propiedad GUID especifica el identificador global único de la conexión.")]
        public string GUID
        {
            get
            {
                return (string)curObj["GUID"];
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsIndexNull
        {
            get
            {
                if (curObj["Index"] == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("La propiedad Index indica el número de índice del adaptador de red, que se almace" +
            "na en el Registro del sistema. \nEjemplo: 0.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public uint Index
        {
            get
            {
                if (curObj["Index"] == null)
                {
                    return Convert.ToUInt32(0);
                }
                return (uint)curObj["Index"];
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsInstallDateNull
        {
            get
            {
                if (curObj["InstallDate"] == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public DateTime InstallDate
        {
            get
            {
                if (curObj["InstallDate"] != null)
                {
                    return ToDateTime((string)curObj["InstallDate"]);
                }
                else
                {
                    return DateTime.MinValue;
                }
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsInstalledNull
        {
            get
            {
                if (curObj["Installed"] == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description(@"La propiedad Installed determina si el adaptador de red está instalado en el sistema.
Valores: TRUE o FALSE. Un valor TRUE indica si el adaptador de red está instalado.  
La propiedad Installed quedó obsoleta. No hay valor de reemplazo y esta propiedad se considera ahora obsoleta.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public bool Installed
        {
            get
            {
                if (curObj["Installed"] == null)
                {
                    return Convert.ToBoolean(0);
                }
                return (bool)curObj["Installed"];
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsInterfaceIndexNull
        {
            get
            {
                if (curObj["InterfaceIndex"] == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("La propiedad InterfaceIndex contiene el valor del índice que identifica de forma " +
            "única a la interfaz local.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public uint InterfaceIndex
        {
            get
            {
                if (curObj["InterfaceIndex"] == null)
                {
                    return Convert.ToUInt32(0);
                }
                return (uint)curObj["InterfaceIndex"];
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsLastErrorCodeNull
        {
            get
            {
                if (curObj["LastErrorCode"] == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("LastErrorCode captura el último código de error informado por el dispositivo lógi" +
            "co.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public uint LastErrorCode
        {
            get
            {
                if (curObj["LastErrorCode"] == null)
                {
                    return Convert.ToUInt32(0);
                }
                return (uint)curObj["LastErrorCode"];
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description(@"La propiedad MACAddress indica la dirección de Media Access Control (MAC) para este adaptador de red. Una dirección MAC es un número único de 48 bits asignado al adaptador de red por el fabricante. Identifica de forma única este adaptador de red y se usa en la asignación de comunicaciones de red TCP/IP.")]
        public string MACAddress
        {
            get
            {
                return (string)curObj["MACAddress"];
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("La propiedad Manufacturer indica el nombre del fabricante del adaptador de red.\nE" +
            "jemplo: 3COM.")]
        public string Manufacturer
        {
            get
            {
                return (string)curObj["Manufacturer"];
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsMaxNumberControlledNull
        {
            get
            {
                if (curObj["MaxNumberControlled"] == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("La propiedad MaxNumberControlled indica el número máximo de los puertos directame" +
            "nte direccionables admitidos por este adaptador de red. Se debe usar el valor ce" +
            "ro si se desconoce el número.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public uint MaxNumberControlled
        {
            get
            {
                if (curObj["MaxNumberControlled"] == null)
                {
                    return Convert.ToUInt32(0);
                }
                return (uint)curObj["MaxNumberControlled"];
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsMaxSpeedNull
        {
            get
            {
                if (curObj["MaxSpeed"] == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("La velocidad máxima, en bits por segundo, para el adaptador de red.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public ulong MaxSpeed
        {
            get
            {
                if (curObj["MaxSpeed"] == null)
                {
                    return Convert.ToUInt64(0);
                }
                return (ulong)curObj["MaxSpeed"];
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Name
        {
            get
            {
                return (string)curObj["Name"];
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("La propiedad NetConnectionID especifica el nombre de conexión de red tal como apa" +
            "rece en la carpeta \"Conexiones de red\".")]
        public string NetConnectionID
        {
            get
            {
                return (string)curObj["NetConnectionID"];
            }
            set
            {
                curObj["NetConnectionID"] = value;
                if (isEmbedded == false
                            && AutoCommitProp == true)
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsNetConnectionStatusNull
        {
            get
            {
                if (curObj["NetConnectionStatus"] == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description(@"NetConnectionStatus es una cadena que indica el estado de la conexión del adaptador de red a la red. El valor de la propiedad se interpreta de la siguiente manera:
0 - Desconectado
1 - Conectar
2 - Conectado
3 - Desconectar
4 - Hardware no presente
5 - Hardware deshabilitado
6 - Hardware con mal funcionamiento
7 - Medios desconectados
8 - Autenticación
9 - Autenticación correcta
10 - Error en la autenticación
11 - Dirección no válida
12 - Credenciales necesarias
.. - Otros - Para valores enteros distintos de los listados más arriba, consulte la documentación de errores de Win32.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public ushort NetConnectionStatus
        {
            get
            {
                if (curObj["NetConnectionStatus"] == null)
                {
                    return Convert.ToUInt16(0);
                }
                return (ushort)curObj["NetConnectionStatus"];
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsNetEnabledNull
        {
            get
            {
                if (curObj["NetEnabled"] == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("La propiedad NetEnabled especifica si está habilitada la conexión de red.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public bool NetEnabled
        {
            get
            {
                if (curObj["NetEnabled"] == null)
                {
                    return Convert.ToBoolean(0);
                }
                return (bool)curObj["NetEnabled"];
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Matriz de cadenas que indica las direcciones de red de un adaptador.")]
        public string[] NetworkAddresses
        {
            get
            {
                return (string[])curObj["NetworkAddresses"];
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description(@"PermanentAddress define la dirección de red dentro del código en un adaptador. Esta dirección ""dentro del código"" puede ser cambiada  vía actualización firmware o configuración de software. Si es así, este campo debe ser actualizado cuando se haga el cambio. PermanentAddress debe dejarse en blanco si no existe dirección ""dentro del código"" en el adaptador de red.")]
        public string PermanentAddress
        {
            get
            {
                return (string)curObj["PermanentAddress"];
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsPhysicalAdapterNull
        {
            get
            {
                if (curObj["PhysicalAdapter"] == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("La propiedad PhysicalAdapter especifica si se trata de un adaptador físico o lógi" +
            "co.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public bool PhysicalAdapter
        {
            get
            {
                if (curObj["PhysicalAdapter"] == null)
                {
                    return Convert.ToBoolean(0);
                }
                return (bool)curObj["PhysicalAdapter"];
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Indica el id. Plug and Play Win32 del dispositivo lógico. Ejemplo: *PNP030b")]
        public string PNPDeviceID
        {
            get
            {
                return (string)curObj["PNPDeviceID"];
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description(@"Indica los recursos específicos relacionados con energía de dispositivo lógico. Los valores de la matriz, 0=""Desconocido"", 1=""No compatible"" y 2=""Deshabilitado"" se explican por sí solos. El valor 3=""Habilitado"" indica que las características de administración de energía están habilitadas actualmente pero se desconoce el conjunto de características exacto o la información no está disponible. "" Modos de ahorro de energía establecidos automáticamente "" (4) describe que un dispositivo puede cambiar su estado de energía con base en el uso u otros criterios. "" Estado de energía configurable "" (5) indica que se admite el método SetPowerState. "" Ciclo de energía permitido "" (6) indica que se puede invocar el método SetPowerState con la variable de entrada PowerState establecida a 5 (""Ciclo de energía ""). "" Se admite el encendido por tiempo "" (7) indica que el método SetPowerState puede ser invocado con la variable de entrada PowerState establecida  a 5 (""Ciclo de energía "") y el parámetro Time establecido a un fecha y hora específica, o intervalo, para encendido.")]
        public PowerManagementCapabilitiesValues[] PowerManagementCapabilities
        {
            get
            {
                Array arrEnumVals = (Array)curObj["PowerManagementCapabilities"];
                PowerManagementCapabilitiesValues[] enumToRet = new PowerManagementCapabilitiesValues[arrEnumVals.Length];
                int counter = 0;
                for (counter = 0; counter < arrEnumVals.Length; counter = counter + 1)
                {
                    enumToRet[counter] = (PowerManagementCapabilitiesValues)Convert.ToInt32(arrEnumVals.GetValue(counter));
                }
                return enumToRet;
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsPowerManagementSupportedNull
        {
            get
            {
                if (curObj["PowerManagementSupported"] == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description(@"Booleano que indica que el Dispositivo se puede administrar con energía - por ej., ponerlo en un estado de ahorro de energía. Este booleano no indica que las características de administración de energía están actualmente habilitadas, o si están deshabilitadas, las características que son compatibles. Consulte la matriz PowerManagementCapabilities para obtener esta información. Si este booleano es falso, el valor entero 1, para la cadena, ""No compatible"", debe ser la única entrada en la matriz PowerManagementCapabilities.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public bool PowerManagementSupported
        {
            get
            {
                if (curObj["PowerManagementSupported"] == null)
                {
                    return Convert.ToBoolean(0);
                }
                return (bool)curObj["PowerManagementSupported"];
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("La propiedad ProductName indica el nombre del producto del adaptador de red.\nEjem" +
            "plo: Fast EtherLink XL")]
        public string ProductName
        {
            get
            {
                return (string)curObj["ProductName"];
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("La propiedad ServiceName indica el nombre de servicio del adaptador de red. Este " +
            "nombre suele ser más corto que el nombre completo del producto. \nEjemplo: Elnkii" +
            ".")]
        public string ServiceName
        {
            get
            {
                return (string)curObj["ServiceName"];
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsSpeedNull
        {
            get
            {
                if (curObj["Speed"] == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Un estimado del ancho de banda actual en bits por segundo. Para extremos que varí" +
            "an en ancho de banda o para aquellos donde no se puede estimar correctamente, es" +
            "ta propiedad debe contener el ancho de banda nominal.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public ulong Speed
        {
            get
            {
                if (curObj["Speed"] == null)
                {
                    return Convert.ToUInt64(0);
                }
                return (ulong)curObj["Speed"];
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Status
        {
            get
            {
                return (string)curObj["Status"];
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsStatusInfoNull
        {
            get
            {
                if (curObj["StatusInfo"] == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description(@"StatusInfo es una cadena que indica si el dispositivo lógico está en un estado habilitado (valor = 3), deshabilitado (valor = 4) o algún otro estado (1) o un estado desconocido (2). Si esta propiedad no se aplica al dispositivo lógico, el valor, 5 (""No aplicable""), debe ser usado.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public StatusInfoValues StatusInfo
        {
            get
            {
                if (curObj["StatusInfo"] == null)
                {
                    return (StatusInfoValues)Convert.ToInt32(0);
                }
                return (StatusInfoValues)Convert.ToInt32(curObj["StatusInfo"]);
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("CreationClassName de ámbito del sistema.")]
        public string SystemCreationClassName
        {
            get
            {
                return (string)curObj["SystemCreationClassName"];
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Nombre del sistema de ámbito.")]
        public string SystemName
        {
            get
            {
                return (string)curObj["SystemName"];
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsTimeOfLastResetNull
        {
            get
            {
                if (curObj["TimeOfLastReset"] == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("La propiedad TimeOfLastReset indica cuándo se restableció por última vez el adapt" +
            "ador de red.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public DateTime TimeOfLastReset
        {
            get
            {
                if (curObj["TimeOfLastReset"] != null)
                {
                    return ToDateTime((string)curObj["TimeOfLastReset"]);
                }
                else
                {
                    return DateTime.MinValue;
                }
            }
        }

        private bool CheckIfProperClass(ManagementScope mgmtScope, ManagementPath path, ObjectGetOptions OptionsParam)
        {
            if (path != null
                        && string.Compare(path.ClassName, ManagementClassName, true, CultureInfo.InvariantCulture) == 0)
            {
                return true;
            }
            else
            {
                return CheckIfProperClass(new ManagementObject(mgmtScope, path, OptionsParam));
            }
        }

        private bool CheckIfProperClass(ManagementBaseObject theObj)
        {
            if (theObj != null
                        && string.Compare((string)theObj["__CLASS"], ManagementClassName, true, CultureInfo.InvariantCulture) == 0)
            {
                return true;
            }
            else
            {
                Array parentClasses = (Array)theObj["__DERIVATION"];
                if (parentClasses != null)
                {
                    int count = 0;
                    for (count = 0; count < parentClasses.Length; count = count + 1)
                    {
                        if (string.Compare((string)parentClasses.GetValue(count), ManagementClassName, true, CultureInfo.InvariantCulture) == 0)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        private bool ShouldSerializeAdapterTypeId()
        {
            if (IsAdapterTypeIdNull == false)
            {
                return true;
            }
            return false;
        }

        private bool ShouldSerializeAutoSense()
        {
            if (IsAutoSenseNull == false)
            {
                return true;
            }
            return false;
        }

        private bool ShouldSerializeAvailability()
        {
            if (IsAvailabilityNull == false)
            {
                return true;
            }
            return false;
        }

        private bool ShouldSerializeConfigManagerErrorCode()
        {
            if (IsConfigManagerErrorCodeNull == false)
            {
                return true;
            }
            return false;
        }

        private bool ShouldSerializeConfigManagerUserConfig()
        {
            if (IsConfigManagerUserConfigNull == false)
            {
                return true;
            }
            return false;
        }

        private bool ShouldSerializeErrorCleared()
        {
            if (IsErrorClearedNull == false)
            {
                return true;
            }
            return false;
        }

        private bool ShouldSerializeIndex()
        {
            if (IsIndexNull == false)
            {
                return true;
            }
            return false;
        }

        // Convierte una fecha y hora determinadas con formato DMTF en un objeto System.DateTime.
        static DateTime ToDateTime(string dmtfDate)
        {
            DateTime initializer = DateTime.MinValue;
            int year = initializer.Year;
            int month = initializer.Month;
            int day = initializer.Day;
            int hour = initializer.Hour;
            int minute = initializer.Minute;
            int second = initializer.Second;
            long ticks = 0;
            string dmtf = dmtfDate;
            DateTime datetime = DateTime.MinValue;
            string tempString = string.Empty;
            if (dmtf == null)
            {
                throw new ArgumentOutOfRangeException();
            }
            if (dmtf.Length == 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            if (dmtf.Length != 25)
            {
                throw new ArgumentOutOfRangeException();
            }
            try
            {
                tempString = dmtf.Substring(0, 4);
                if ("****" != tempString)
                {
                    year = int.Parse(tempString);
                }
                tempString = dmtf.Substring(4, 2);
                if ("**" != tempString)
                {
                    month = int.Parse(tempString);
                }
                tempString = dmtf.Substring(6, 2);
                if ("**" != tempString)
                {
                    day = int.Parse(tempString);
                }
                tempString = dmtf.Substring(8, 2);
                if ("**" != tempString)
                {
                    hour = int.Parse(tempString);
                }
                tempString = dmtf.Substring(10, 2);
                if ("**" != tempString)
                {
                    minute = int.Parse(tempString);
                }
                tempString = dmtf.Substring(12, 2);
                if ("**" != tempString)
                {
                    second = int.Parse(tempString);
                }
                tempString = dmtf.Substring(15, 6);
                if ("******" != tempString)
                {
                    ticks = long.Parse(tempString) * (TimeSpan.TicksPerMillisecond / 1000);
                }
                if (year < 0
                            || month < 0
                            || day < 0
                            || hour < 0
                            || minute < 0
                            || minute < 0
                            || second < 0
                            || ticks < 0)
                {
                    throw new ArgumentOutOfRangeException();
                }
            }
            catch (Exception e)
            {
                throw new ArgumentOutOfRangeException(null, e.Message);
            }
            datetime = new DateTime(year, month, day, hour, minute, second, 0);
            datetime = datetime.AddTicks(ticks);
            TimeSpan tickOffset = TimeZone.CurrentTimeZone.GetUtcOffset(datetime);
            int UTCOffset = 0;
            int OffsetToBeAdjusted = 0;
            long OffsetMins = tickOffset.Ticks / TimeSpan.TicksPerMinute;
            tempString = dmtf.Substring(22, 3);
            if (tempString != "******")
            {
                tempString = dmtf.Substring(21, 4);
                try
                {
                    UTCOffset = int.Parse(tempString);
                }
                catch (Exception e)
                {
                    throw new ArgumentOutOfRangeException(null, e.Message);
                }
                OffsetToBeAdjusted = (int)(OffsetMins - UTCOffset);
                datetime = datetime.AddMinutes(OffsetToBeAdjusted);
            }
            return datetime;
        }

        // Convierte un objeto System.DateTime determinado al formato de fecha y hora DMTF.
        static string ToDmtfDateTime(DateTime date)
        {
            string utcString = string.Empty;
            TimeSpan tickOffset = TimeZone.CurrentTimeZone.GetUtcOffset(date);
            long OffsetMins = tickOffset.Ticks / TimeSpan.TicksPerMinute;
            if (Math.Abs(OffsetMins) > 999)
            {
                date = date.ToUniversalTime();
                utcString = "+000";
            }
            else
            {
                if (tickOffset.Ticks >= 0)
                {
                    utcString = string.Concat("+", (tickOffset.Ticks / TimeSpan.TicksPerMinute).ToString().PadLeft(3, '0'));
                }
                else
                {
                    string strTemp = OffsetMins.ToString();
                    utcString = string.Concat("-", strTemp.Substring(1, strTemp.Length - 1).PadLeft(3, '0'));
                }
            }
            string dmtfDateTime = date.Year.ToString().PadLeft(4, '0');
            dmtfDateTime = string.Concat(dmtfDateTime, date.Month.ToString().PadLeft(2, '0'));
            dmtfDateTime = string.Concat(dmtfDateTime, date.Day.ToString().PadLeft(2, '0'));
            dmtfDateTime = string.Concat(dmtfDateTime, date.Hour.ToString().PadLeft(2, '0'));
            dmtfDateTime = string.Concat(dmtfDateTime, date.Minute.ToString().PadLeft(2, '0'));
            dmtfDateTime = string.Concat(dmtfDateTime, date.Second.ToString().PadLeft(2, '0'));
            dmtfDateTime = string.Concat(dmtfDateTime, ".");
            DateTime dtTemp = new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second, 0);
            long microsec = (date.Ticks - dtTemp.Ticks)
                        * 1000
                        / TimeSpan.TicksPerMillisecond;
            string strMicrosec = microsec.ToString();
            if (strMicrosec.Length > 6)
            {
                strMicrosec = strMicrosec.Substring(0, 6);
            }
            dmtfDateTime = string.Concat(dmtfDateTime, strMicrosec.PadLeft(6, '0'));
            dmtfDateTime = string.Concat(dmtfDateTime, utcString);
            return dmtfDateTime;
        }

        private bool ShouldSerializeInstallDate()
        {
            if (IsInstallDateNull == false)
            {
                return true;
            }
            return false;
        }

        private bool ShouldSerializeInstalled()
        {
            if (IsInstalledNull == false)
            {
                return true;
            }
            return false;
        }

        private bool ShouldSerializeInterfaceIndex()
        {
            if (IsInterfaceIndexNull == false)
            {
                return true;
            }
            return false;
        }

        private bool ShouldSerializeLastErrorCode()
        {
            if (IsLastErrorCodeNull == false)
            {
                return true;
            }
            return false;
        }

        private bool ShouldSerializeMaxNumberControlled()
        {
            if (IsMaxNumberControlledNull == false)
            {
                return true;
            }
            return false;
        }

        private bool ShouldSerializeMaxSpeed()
        {
            if (IsMaxSpeedNull == false)
            {
                return true;
            }
            return false;
        }

        private void ResetNetConnectionID()
        {
            curObj["NetConnectionID"] = null;
            if (isEmbedded == false
                        && AutoCommitProp == true)
            {
                PrivateLateBoundObject.Put();
            }
        }

        private bool ShouldSerializeNetConnectionStatus()
        {
            if (IsNetConnectionStatusNull == false)
            {
                return true;
            }
            return false;
        }

        private bool ShouldSerializeNetEnabled()
        {
            if (IsNetEnabledNull == false)
            {
                return true;
            }
            return false;
        }

        private bool ShouldSerializePhysicalAdapter()
        {
            if (IsPhysicalAdapterNull == false)
            {
                return true;
            }
            return false;
        }

        private bool ShouldSerializePowerManagementSupported()
        {
            if (IsPowerManagementSupportedNull == false)
            {
                return true;
            }
            return false;
        }

        private bool ShouldSerializeSpeed()
        {
            if (IsSpeedNull == false)
            {
                return true;
            }
            return false;
        }

        private bool ShouldSerializeStatusInfo()
        {
            if (IsStatusInfoNull == false)
            {
                return true;
            }
            return false;
        }

        private bool ShouldSerializeTimeOfLastReset()
        {
            if (IsTimeOfLastResetNull == false)
            {
                return true;
            }
            return false;
        }

        [Browsable(true)]
        public void CommitObject()
        {
            if (isEmbedded == false)
            {
                PrivateLateBoundObject.Put();
            }
        }

        [Browsable(true)]
        public void CommitObject(PutOptions putOptions)
        {
            if (isEmbedded == false)
            {
                PrivateLateBoundObject.Put(putOptions);
            }
        }

        private void Initialize()
        {
            AutoCommitProp = true;
            isEmbedded = false;
        }

        private static string ConstructPath(string keyDeviceID)
        {
            string strPath = "root\\CimV2:Win32_NetworkAdapter";
            strPath = string.Concat(strPath, string.Concat(".DeviceID=", string.Concat("\"", string.Concat(keyDeviceID, "\""))));
            return strPath;
        }

        private void InitializeObject(ManagementScope mgmtScope, ManagementPath path, ObjectGetOptions getOptions)
        {
            Initialize();
            if (path != null)
            {
                if (CheckIfProperClass(mgmtScope, path, getOptions) != true)
                {
                    throw new ArgumentException("El nombre de clase no coincide.");
                }
            }
            PrivateLateBoundObject = new ManagementObject(mgmtScope, path, getOptions);
            PrivateSystemProperties = new ManagementSystemProperties(PrivateLateBoundObject);
            curObj = PrivateLateBoundObject;
        }

        // Diferentes sobrecargas de ayuda GetInstances() para enumerar instancias de la clase WMI.
        public static NetworkAdapterCollection GetInstances()
        {
            return GetInstances(null, null, null);
        }

        public static NetworkAdapterCollection GetInstances(string condition)
        {
            return GetInstances(null, condition, null);
        }

        public static NetworkAdapterCollection GetInstances(string[] selectedProperties)
        {
            return GetInstances(null, null, selectedProperties);
        }

        public static NetworkAdapterCollection GetInstances(string condition, string[] selectedProperties)
        {
            return GetInstances(null, condition, selectedProperties);
        }

        public static NetworkAdapterCollection GetInstances(ManagementScope mgmtScope, EnumerationOptions enumOptions)
        {
            if (mgmtScope == null)
            {
                if (statMgmtScope == null)
                {
                    mgmtScope = new ManagementScope();
                    mgmtScope.Path.NamespacePath = "root\\CimV2";
                }
                else
                {
                    mgmtScope = statMgmtScope;
                }
            }
            ManagementPath pathObj = new ManagementPath();
            pathObj.ClassName = "Win32_NetworkAdapter";
            pathObj.NamespacePath = "root\\CimV2";
            ManagementClass clsObject = new ManagementClass(mgmtScope, pathObj, null);
            if (enumOptions == null)
            {
                enumOptions = new EnumerationOptions();
                enumOptions.EnsureLocatable = true;
            }
            return new NetworkAdapterCollection(clsObject.GetInstances(enumOptions));
        }

        public static NetworkAdapterCollection GetInstances(ManagementScope mgmtScope, string condition)
        {
            return GetInstances(mgmtScope, condition, null);
        }

        public static NetworkAdapterCollection GetInstances(ManagementScope mgmtScope, string[] selectedProperties)
        {
            return GetInstances(mgmtScope, null, selectedProperties);
        }

        public static NetworkAdapterCollection GetInstances(ManagementScope mgmtScope, string condition, string[] selectedProperties)
        {
            if (mgmtScope == null)
            {
                if (statMgmtScope == null)
                {
                    mgmtScope = new ManagementScope();
                    mgmtScope.Path.NamespacePath = "root\\CimV2";
                }
                else
                {
                    mgmtScope = statMgmtScope;
                }
            }
            ManagementObjectSearcher ObjectSearcher = new ManagementObjectSearcher(mgmtScope, new SelectQuery("Win32_NetworkAdapter", condition, selectedProperties));
            EnumerationOptions enumOptions = new EnumerationOptions();
            enumOptions.EnsureLocatable = true;
            ObjectSearcher.Options = enumOptions;
            return new NetworkAdapterCollection(ObjectSearcher.Get());
        }

        [Browsable(true)]
        public static NetworkAdapter CreateInstance()
        {
            ManagementScope mgmtScope = null;
            if (statMgmtScope == null)
            {
                mgmtScope = new ManagementScope();
                mgmtScope.Path.NamespacePath = CreatedWmiNamespace;
            }
            else
            {
                mgmtScope = statMgmtScope;
            }
            ManagementPath mgmtPath = new ManagementPath(CreatedClassName);
            ManagementClass tmpMgmtClass = new ManagementClass(mgmtScope, mgmtPath, null);
            return new NetworkAdapter(tmpMgmtClass.CreateInstance());
        }

        [Browsable(true)]
        public void Delete()
        {
            PrivateLateBoundObject.Delete();
        }

        public uint Disable()
        {
            if (isEmbedded == false)
            {
                ManagementBaseObject inParams = null;
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("Disable", inParams, null);
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                return Convert.ToUInt32(0);
            }
        }

        public uint Enable()
        {
            if (isEmbedded == false)
            {
                ManagementBaseObject inParams = null;
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("Enable", inParams, null);
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                return Convert.ToUInt32(0);
            }
        }

        public uint Reset()
        {
            if (isEmbedded == false)
            {
                ManagementBaseObject inParams = null;
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("Reset", inParams, null);
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                return Convert.ToUInt32(0);
            }
        }

        public uint SetPowerState(ushort PowerState, DateTime Time)
        {
            if (isEmbedded == false)
            {
                ManagementBaseObject inParams = null;
                inParams = PrivateLateBoundObject.GetMethodParameters("SetPowerState");
                inParams["PowerState"] = PowerState;
                inParams["Time"] = ToDmtfDateTime(Time);
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("SetPowerState", inParams, null);
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                return Convert.ToUInt32(0);
            }
        }

        public enum AdapterTypeIdValues
        {

            Ethernet_802_3 = 0,

            Token_Ring_802_5 = 1,

            Interfaz_de_datos_de_distribución_por_fibra_FDDI_ = 2,

            Red_de_área_extensa_WAN_ = 3,

            LocalTalk = 4,

            Ethernet_usando_el_formato_DIX_de_encabezado = 5,

            ARCNET = 6,

            ARCNET_878_2_ = 7,

            ATM = 8,

            Inalámbrico = 9,

            Inalámbrico_de_infrarrojos = 10,

            Bpc = 11,

            CoWan = 12,

            Val_1394 = 13,

            NULL_ENUM_VALUE = 14,
        }

        public enum AvailabilityValues
        {

            Otros = 1,

            Desconocido = 2,

            Funcionar_Energía_completa = 3,

            Advertencia = 4,

            En_prueba = 5,

            No_aplicable = 6,

            Apagado = 7,

            Sin_conexión_a_la_red = 8,

            Inactivo = 9,

            Degradado = 10,

            No_instalado = 11,

            Error_de_instalación = 12,

            Ahorro_de_energía_desconocido = 13,

            Ahorro_de_energía_modo_de_bajo_consumo = 14,

            Ahorro_de_energía_espera = 15,

            Ciclo_de_energía = 16,

            Ahorro_de_energía_advertencia = 17,

            Pausado = 18,

            No_está_listo = 19,

            No_configurado = 20,

            Inactivo0 = 21,

            NULL_ENUM_VALUE = 0,
        }

        public enum ConfigManagerErrorCodeValues
        {

            Este_dispositivo_funciona_correctamente_ = 0,

            El_dispositivo_no_está_configurado_correctamente_ = 1,

            Windows_no_puede_cargar_el_controlador_para_este_dispositivo_ = 2,

            El_controlador_de_este_dispositivo_podría_estar_dañado_o_es_posible_que_su_sistema_tenga_poca_memoria_u_otros_recursos_ = 3,

            Este_dispositivo_no_funciona_correctamente_Podría_estar_dañado_uno_de_sus_controladores_o_el_Registro_ = 4,

            El_controlador_de_este_dispositivo_necesita_un_recurso_que_Windows_no_puede_administrar_ = 5,

            La_configuración_de_arranque_de_este_dispositivo_está_en_conflicto_con_otros_dispositivos_ = 6,

            No_se_puede_filtrar_ = 7,

            Falta_el_controlador_del_dispositivo_ = 8,

            Este_dispositivo_no_funciona_correctamente_porque_el_firmware_de_control_informa_incorrectamente_de_los_recursos_del_dispositivo_ = 9,

            No_puede_iniciar_este_dispositivo_ = 10,

            Error_de_este_dispositivo_ = 11,

            Este_dispositivo_no_encuentra_suficientes_recursos_libres_que_pueda_usar_ = 12,

            Windows_no_puede_comprobar_los_recursos_de_este_dispositivo_ = 13,

            El_dispositivo_no_puede_funcionar_correctamente_hasta_que_reinicie_su_equipo_ = 14,

            Este_dispositivo_no_funciona_correctamente_porque_quizá_existe_un_problema_de_reenumeración_ = 15,

            Windows_no_puede_identificar_todos_los_recursos_que_usa_este_dispositivo_ = 16,

            Este_dispositivo_está_solicitando_un_tipo_de_recurso_desconocido_ = 17,

            Reinstalar_los_controladores_de_este_dispositivo_ = 18,

            Error_al_usar_el_cargador_VxD_ = 19,

            Su_Registro_podría_estar_dañado_ = 20,

            Error_del_sistema_pruebe_a_cambiar_el_controlador_de_este_dispositivo_Si_eso_no_funciona_consulte_la_documentación_del_hardware_Windows_quitará_este_dispositivo_ = 21,

            Este_dispositivo_está_deshabilitado_ = 22,

            Error_del_sistema_pruebe_a_cambiar_el_controlador_de_este_dispositivo_Si_eso_no_funciona_consulte_la_documentación_del_hardware_ = 23,

            Este_dispositivo_no_está_presente_no_funciona_correctamente_o_no_tiene_todos_sus_controladores_instalados_ = 24,

            Windows_sigue_configurando_este_dispositivo_ = 25,

            Windows_sigue_configurando_este_dispositivo_0 = 26,

            Este_dispositivo_no_tiene_una_configuración_de_registro_válida_ = 27,

            Los_controladores_de_este_dispositivo_no_están_instalados_ = 28,

            Este_dispositivo_está_deshabilitado_porque_su_firmware_no_le_proporcionó_los_recursos_requeridos_ = 29,

            Este_dispositivo_usa_un_recurso_de_solicitud_de_interrupción_IRQ_que_usa_otro_dispositivo_ = 30,

            Este_dispositivo_no_funciona_correctamente_porque_Windows_no_puede_cargar_los_controladores_requeridos_para_este_dispositivo_ = 31,

            NULL_ENUM_VALUE = 32,
        }

        public enum PowerManagementCapabilitiesValues
        {

            Desconocido = 0,

            No_compatible = 1,

            Deshabilitado = 2,

            Habilitado = 3,

            Modos_de_ahorro_de_energía_establecidos_automáticamente = 4,

            Estado_de_energía_configurable = 5,

            Ciclo_de_energía_permitido = 6,

            Se_admite_el_encendido_por_tiempo = 7,

            NULL_ENUM_VALUE = 8,
        }

        public enum StatusInfoValues
        {

            Otros = 1,

            Desconocido = 2,

            Habilitado = 3,

            Deshabilitado = 4,

            No_aplicable = 5,

            NULL_ENUM_VALUE = 0,
        }

        // Implementación del enumerador para enumerar instancias de la clase.
        public class NetworkAdapterCollection : object, ICollection
        {

            private ManagementObjectCollection privColObj;

            public NetworkAdapterCollection(ManagementObjectCollection objCollection)
            {
                privColObj = objCollection;
            }

            public virtual int Count
            {
                get
                {
                    return privColObj.Count;
                }
            }

            public virtual bool IsSynchronized
            {
                get
                {
                    return privColObj.IsSynchronized;
                }
            }

            public virtual object SyncRoot
            {
                get
                {
                    return this;
                }
            }

            public virtual void CopyTo(Array array, int index)
            {
                privColObj.CopyTo(array, index);
                int nCtr;
                for (nCtr = 0; nCtr < array.Length; nCtr = nCtr + 1)
                {
                    array.SetValue(new NetworkAdapter((ManagementObject)array.GetValue(nCtr)), nCtr);
                }
            }

            public virtual IEnumerator GetEnumerator()
            {
                return new NetworkAdapterEnumerator(privColObj.GetEnumerator());
            }

            public class NetworkAdapterEnumerator : object, IEnumerator
            {

                private ManagementObjectCollection.ManagementObjectEnumerator privObjEnum;

                public NetworkAdapterEnumerator(ManagementObjectCollection.ManagementObjectEnumerator objEnum)
                {
                    privObjEnum = objEnum;
                }

                public virtual object Current
                {
                    get
                    {
                        return new NetworkAdapter((ManagementObject)privObjEnum.Current);
                    }
                }

                public virtual bool MoveNext()
                {
                    return privObjEnum.MoveNext();
                }

                public virtual void Reset()
                {
                    privObjEnum.Reset();
                }
            }
        }

        // Elemento TypeConverter que administra valores NULL para propiedades ValueType
        public class WMIValueTypeConverter : TypeConverter
        {

            private TypeConverter baseConverter;

            private Type baseType;

            public WMIValueTypeConverter(Type inBaseType)
            {
                baseConverter = TypeDescriptor.GetConverter(inBaseType);
                baseType = inBaseType;
            }

            public override bool CanConvertFrom(ITypeDescriptorContext context, Type srcType)
            {
                return baseConverter.CanConvertFrom(context, srcType);
            }

            public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
            {
                return baseConverter.CanConvertTo(context, destinationType);
            }

            public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
            {
                return baseConverter.ConvertFrom(context, culture, value);
            }

            public override object CreateInstance(ITypeDescriptorContext context, IDictionary dictionary)
            {
                return baseConverter.CreateInstance(context, dictionary);
            }

            public override bool GetCreateInstanceSupported(ITypeDescriptorContext context)
            {
                return baseConverter.GetCreateInstanceSupported(context);
            }

            public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributeVar)
            {
                return baseConverter.GetProperties(context, value, attributeVar);
            }

            public override bool GetPropertiesSupported(ITypeDescriptorContext context)
            {
                return baseConverter.GetPropertiesSupported(context);
            }

            public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
            {
                return baseConverter.GetStandardValues(context);
            }

            public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
            {
                return baseConverter.GetStandardValuesExclusive(context);
            }

            public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
            {
                return baseConverter.GetStandardValuesSupported(context);
            }

            public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
            {
                if (baseType.BaseType == typeof(Enum))
                {
                    if (value.GetType() == destinationType)
                    {
                        return value;
                    }
                    if (value == null
                                && context != null
                                && context.PropertyDescriptor.ShouldSerializeValue(context.Instance) == false)
                    {
                        return "NULL_ENUM_VALUE";
                    }
                    return baseConverter.ConvertTo(context, culture, value, destinationType);
                }
                if (baseType == typeof(bool)
                            && baseType.BaseType == typeof(ValueType))
                {
                    if (value == null
                                && context != null
                                && context.PropertyDescriptor.ShouldSerializeValue(context.Instance) == false)
                    {
                        return "";
                    }
                    return baseConverter.ConvertTo(context, culture, value, destinationType);
                }
                if (context != null
                            && context.PropertyDescriptor.ShouldSerializeValue(context.Instance) == false)
                {
                    return "";
                }
                return baseConverter.ConvertTo(context, culture, value, destinationType);
            }
        }

        // Clase incrustada que representa las propiedades WMI del sistema.
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public class ManagementSystemProperties
        {

            private ManagementBaseObject PrivateLateBoundObject;

            public ManagementSystemProperties(ManagementBaseObject ManagedObject)
            {
                PrivateLateBoundObject = ManagedObject;
            }

            [Browsable(true)]
            public int GENUS
            {
                get
                {
                    return (int)PrivateLateBoundObject["__GENUS"];
                }
            }

            [Browsable(true)]
            public string CLASS
            {
                get
                {
                    return (string)PrivateLateBoundObject["__CLASS"];
                }
            }

            [Browsable(true)]
            public string SUPERCLASS
            {
                get
                {
                    return (string)PrivateLateBoundObject["__SUPERCLASS"];
                }
            }

            [Browsable(true)]
            public string DYNASTY
            {
                get
                {
                    return (string)PrivateLateBoundObject["__DYNASTY"];
                }
            }

            [Browsable(true)]
            public string RELPATH
            {
                get
                {
                    return (string)PrivateLateBoundObject["__RELPATH"];
                }
            }

            [Browsable(true)]
            public int PROPERTY_COUNT
            {
                get
                {
                    return (int)PrivateLateBoundObject["__PROPERTY_COUNT"];
                }
            }

            [Browsable(true)]
            public string[] DERIVATION
            {
                get
                {
                    return (string[])PrivateLateBoundObject["__DERIVATION"];
                }
            }

            [Browsable(true)]
            public string SERVER
            {
                get
                {
                    return (string)PrivateLateBoundObject["__SERVER"];
                }
            }

            [Browsable(true)]
            public string NAMESPACE
            {
                get
                {
                    return (string)PrivateLateBoundObject["__NAMESPACE"];
                }
            }

            [Browsable(true)]
            public string PATH
            {
                get
                {
                    return (string)PrivateLateBoundObject["__PATH"];
                }
            }
        }
    }
}
