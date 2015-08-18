using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AutoPoco;
using AutoPoco.DataSources;
using AutoPoco.Engine;
using e10.Shared.Security;
using Lucene.Net.Linq;
using Newtonsoft.Json;
using Talent21.Data.Core;
using Talent21.Data.Repository;
using Talent21.Service.Abstraction;
using Talent21.Service.Models;

namespace Talent21.Service.Core
{
    public class CountryData
    {
        [JsonProperty(PropertyName = "c")]
        public string Code { get; set; }
        [JsonProperty(PropertyName = "t")]
        public string Title { get; set; }
    }


    public class DateTimeNullableSource : DatasourceBase<DateTime?>
    {
        private readonly DateTime _min;
        private readonly DateTime _max;
        private readonly Random _random = new Random(1337);

        public DateTimeNullableSource()
        {
            _min = DateTime.MinValue;
            _max = DateTime.MaxValue;
        }

        public DateTimeNullableSource(DateTime min, DateTime max)
        {
            _min = min;
            _max = max;
        }

        public override DateTime? Next(IGenerationContext context)
        {
            var range = (_max - _min).Ticks;
            var ticks = (long)(_random.NextDouble() * range);
            return _min.AddTicks(ticks);
        }
    }
    public class JobTitleSource : DatasourceBase<string>
    {
        private readonly Random _random = new Random(1337);

        public override string Next(IGenerationContext context)
        {
            return Data[_random.Next(0, Data.Length)];
        }

        private static readonly string[] Data =
        {
            "Business Analyst, Support",
            "Technical Analyst",
            "Associate Dean Management",
            "Opening For Fresher Data Entry Operators",
            "AM HSBC Advance",
            "Associate Technical Account Manager",
            ".Net Developer (Network Telepony)",
            "Senior Software Engineer",
            ".Net Developer",
            "Software Programmer",
            "Sharepoint Developer",
            "Jr. Software Engineer",
            "Core Java Developer",
            "Android&Ios App Developers fresher",
            "Radiologist / Skin Specialist / Dentist",
            "Patient Counselor",
            "Programme Coordinator",
            "Project Manager",
            "eCommerce Assistant",
            ""
        };
    }
    public class IndianCitySource : DatasourceBase<String>
    {
        private readonly Random _random = new Random(1337);

        public override string Next(IGenerationContext context)
        {
            return Cities[_random.Next(0, Cities.Length)];
        }

        private static readonly string[] Cities =
        {
            "Agartala", "Agra", "Ahmedabad", "Ahmednagar", "Aizawl", "Ajmer", "Akola", "Aligarh", "Allahabad", "Alwar",
            "Ambattur", "Ambernath", "Amravati", "Amritsar", "Anantapur", "Anantnag", "Arrah", "Asansol", "Aurangabad",
            "Avadi", "Bally", "Bangalore", "Baranagar", "Barasat", "Bardhaman", "Bareilly", "Bathinda", "Begusarai",
            "Belgaum", "Bellary", "Bhagalpur", "Bharatpur", "Bhatpara", "Bhavnagar", "Bhilai", "Bhilwara", "Bhiwandi",
            "Bhopal", "Bhubaneswar", "Bihar Sharif", "Bijapur", "Bikaner", "Bilaspur", "Bokaro", "Brahmapur",
            "Bulandshahr", "Chandigarh", "Chandrapur", "Chennai", "Coimbatore", "Cuttack", "Darbhanga", "Davanagere",
            "Dehradun", "Delhi", "Dewas", "Dhanbad", "Dhule", "Durg", "Durgapur", "Etawah", "Faridabad", "Farrukhabad",
            "Firozabad", "Gandhidham", "Gaya", "Ghaziabad", "Gopalpur", "Gorakhpur", "Gulbarga", "Guntur", "Gurgaon",
            "Guwahati", "Gwalior", "Hapur", "Haridwar", "Hisar", "Howrah", "Hubballi-Dharwad", "Hyderabad",
            "Ichalkaranji", "Imphal", "Indore", "Jabalpur", "Jaipur", "Jalandhar", "Jalgaon", "Jalna", "Jammu",
            "Jamnagar", "Jamshedpur", "Jhansi", "Jodhpur", "Junagadh", "Kadapa", "Kakinada", "Kalyan-Dombivali",
            "Kamarhati", "Kanpur", "Karawal Nagar", "Karimnagar", "Karnal", "Katihar", "Khammam", "Kirari Suleman Nagar",
            "Kochi", "Kolhapur", "Kolkata", "Kollam", "Korba", "Kota", "Kozhikode", "Kulti", "Kurnool", "Latur", "Loni",
            "Lucknow", "Ludhiana", "Madurai", "Maheshtala", "Malegaon", "Mangalore", "Mango", "Mathura", "Mau", "Meerut",
            "Mira-Bhayandar", "Mirzapur", "Moradabad", "Mumbai", "Muzaffarnagar", "Muzaffarpur", "Mysore", "Nagercoil",
            "Nagpur", "Nanded", "Nashik", "Navi Mumbai", "Nellore", "New Delhi", "Nizamabad", "Noida", "North Dumdum",
            "Ozhukarai", "Pali", "Panihati", "Panipat", "Parbhani", "Patiala", "Patna", "Pimpri-Chinchwad", "Puducherry",
            "Pune", "Purnia", "Raichur", "Raipur", "Rajahmundry", "Rajkot", "Rajpur Sonarpur", "Rampur", "Ranchi",
            "Ratlam", "Rewa", "Rohtak", "Rourkela", "Sagar", "Saharanpur", "Salem", "Sangli-Miraj & Kupwad", "Satna",
            "Shahjahanpur", "Sikar", "Siliguri", "Solapur", "Sonipat", "South Dumdum", "Sri Ganganagar", "Srinagar",
            "Surat", "Thane", "Thanjavur", "Thiruvananthapuram", "Thoothukudi", "Thrissur", "Tiruchirappalli",
            "Tirunelveli", "Tirupati", "Tirupur", "Tiruvottiyur", "Tumkur", "Udaipur", "Ujjain", "Ulhasnagar",
            "Vadodara", "Varanasi", "Vasai-Virar", "Vijayawada", "Visakhapatnam", "Vizianagaram", "Warangal"
        };
    }
    public class GangFirstNameSource : DatasourceBase<string>
    {
        private readonly Random _random = new Random(1337);

        public override string Next(IGenerationContext context)
        {
            return Middlenames[_random.Next(0, Middlenames.Length)];
        }

        private static readonly string[] Middlenames = new[]{
             "Chikna",
             "Hatela",
             "Shatkela",
             "Thakela",
             "Yeda",
             "Chhatri",
        };
    }

    public class DictionarySource : DatasourceBase<int?>
    {
        private readonly Random _random = new Random(1337);

        public override int? Next(IGenerationContext context)
        {
            return Data[_random.Next(0, Data.Length)].Id;
        }

        private readonly DictionaryEditViewModel[] Data;
        public DictionarySource(DictionaryEditViewModel[] data)
        {
            Data = data;
        }
    }

    public class DictionaryCodeSource : DatasourceBase<string>
    {
        private readonly Random _random = new Random(1337);

        public override string Next(IGenerationContext context)
        {
            return Data[_random.Next(0, Data.Length)].Title;
        }

        private readonly DictionaryEditViewModel[] Data;
        public DictionaryCodeSource(DictionaryEditViewModel[] data)
        {
            Data = data;
        }
    }

    public class GangLastNameSource : DatasourceBase<string>
    {
        private readonly Random _random = new Random(1337);

        public override string Next(IGenerationContext context)
        {
            return Middlenames[_random.Next(0, Middlenames.Length)];
        }

        private static readonly string[] Middlenames = new[]{
            "Kaliya",
            "Dokya",
            "Supari",
            "Chhote",
            "Chhota",
            "Raju",
            "Lallan",
            "Bada",
            "Ustad",
            "Usman",
            "Ali",
            "Khabri",
            "Shana",
            "Bhai",
            "Takla",
            "Hakla",
            "Batli",
            "Pakya",
            "Gotya"
        };
    }

    public class DemoDataService : IDemoDataService
    {
        private readonly ICompanyService _companyService;
        private readonly IContractorService _contractorService;
        private readonly ISystemService _systemService;
        private readonly IUserService _userService;

        private const string _contractor = "Contractor";
        private const string _company = "Company";

        private readonly Random _random = new Random(1337);

        public DemoDataService(ICompanyService companyService, IContractorService contractorService, ISystemService systemService, IUserService userService)
        {
            _companyService = companyService;
            _contractorService = contractorService;
            _systemService = systemService;
            _userService = userService;
        }

        public void BuildMaster()
        {
            var industriesText = new[]
            {
                "Accounting/Finance", "Advertising/PR/MR/Events", "Agriculture/Dairy", "Animation",
                "Architecture/Interior Designing", "Auto/Auto Ancillary", "Aviation / Aerospace Firms",
                "Banking/Financial Services/Broking", "BPO/ITES", "Brewery / Distillery", "Broadcasting",
                "Ceramics /Sanitary ware", "Chemicals/PetroChemical/Plastic/Rubber",
                "Construction/Engineering/Cement/Metals", "Consumer Durables", "Courier/Transportation/Freight",
                "Defence/Government", "Education/Teaching/Training", "Electricals / Switchgears", "Export/Import",
                "Facility Management", "Fertilizers/Pesticides", "FMCG/Foods/Beverage", "Food Processing",
                "Fresher/Trainee", "Gems & Jewellery", "Glass", "Heat Ventilation Air Conditioning",
                "Hotels/Restaurants/Airlines/Travel", "Industrial Products/Heavy Machinery", "Insurance",
                "Internet/Ecommerce", "IT-Hardware & Networking", "IT-Software/Software Services",
                "KPO / Research /Analytics", "Leather", "Legal", "Media/Dotcom/Entertainment",
                "Medical/Healthcare/Hospital", "Mining", "NGO/Social Services", "Office Equipment/Automation",
                "Oil and Gas/Power/Infrastructure/Energy", "Paper", "Pharma/Biotech/Clinical Research",
                "Printing/Packaging", "Publishing", "Real Estate/Property", "Recruitment", "Retail",
                "Security/Law Enforcement", "Semiconductors/Electronics", "Shipping/Marine", "Steel",
                "Strategy /Management Consulting Firms", "Sugar", "Telcom/ISP", "Textiles/Garments/Accessories", "Tyres",
                "Water Treatment / Waste Management", "Wellness/Fitness/Sports", "Other"
            };

            var pattern = new Regex("[^a-z0-9#\\+]", RegexOptions.Multiline & RegexOptions.IgnoreCase);
            if (!_systemService.Industries.Any())
            {
                foreach (var industry in industriesText)
                {
                    _systemService.Create(new IndustryDictionaryCreateViewModel
                    {
                        Code = pattern.Replace(industry.ToLower(), ""),
                        Title = industry
                    });
                }
            }

            var areasText = new[]
            {
                "Accounts / Finance / Tax / CS / Audit", "Agent", "Analytics & Business Intelligence",
                "Architecture / Interior Design", "Banking / Insurance", "Content / Journalism",
                "Corporate Planning / Consulting", "Engineering Design / R&D", "Export / Import / Merchandising",
                "Fashion / Garments / Merchandising", "Guards / Security Services", "Hotels / Restaurants",
                "HR / Administration / IR", "IT Software - Client Server", "IT Software - Mainframe",
                "IT Software - Middleware", "IT Software - Mobile", "IT Software - Other",
                "IT Software - System Programming", "IT Software - Telecom Software",
                "IT Software - Application Programming / Maintenance", "IT Software - DBA / Datawarehousing",
                "IT Software - E-Commerce / Internet Technologies", "IT Software - Embedded /EDA /VLSI /ASIC /Chip Des.",
                "IT Software - ERP / CRM", "IT Software - Network Administration / Security",
                "IT Software - QA & Testing", "IT Software - Systems / EDP / MIS",
                "IT- Hardware / Telecom / Technical Staff / Support", "ITES / BPO / KPO / Customer Service / Operations",
                "Legal", "Marketing / Advertising / MR / PR", "Packaging",
                "Pharma / Biotech / Healthcare / Medical / R&D", "Production / Maintenance / Quality",
                "Purchase / Logistics / Supply Chain", "Sales / BD", "Secretary / Front Office / Data Entry",
                "Self Employed / Consultants", "Shipping", "Site Engineering / Project Management",
                "Teaching / Education", "Ticketing / Travel / Airlines", "Top Management", "TV / Films / Production",
                "Web / Graphic Design / Visualiser", "Other"
            };
            if (!_systemService.FunctionalAreas.Any())
            {
                foreach (var x in areasText)
                {
                    _systemService.Create(new FunctionalAreaDictionaryCreateViewModel
                    {
                        Code = pattern.Replace(x.ToLower(), ""),
                        Title = x
                    });
                }
            }


            var skillText = new[]
            {
                ".NET", "4D", "Active Directory", "Agile Development", "AJAX", "Amazon Web Services", "Analytics",
                "Angular.js", "Apache", "Apache Solr", "Apple Safari", "AS400 & iSeries", "ASP", "ASP.NET", "Assembly",
                "Asterisk PBX", "AutoHotkey", "Azure", "backbone.js", "Balsamiq", "Big Data", "BigCommerce", "Bitcoin",
                "Biztalk", "Blog Install", "BMC Remedy", "Boonex Dolphin", "BSD", "Business Catalyst", "C Programming",
                "C# Programming", "C++ Programming", "CakePHP", "Call Control XML", "CasperJS",
                "Chef Configuration Management", "Chordiant", "Chrome OS", "Cisco", "Cloud Computing", "CMS", "COBOL",
                "Cocoa", "Codeigniter", "Cold Fusion", "Computer Graphics", "Computer Security",
                "Conversion Rate Optimisation", "CRE Loaded", "CS-Cart", "CubeCart", "CUDA", "D3.js", "Dart",
                "Data Warehousing", "Database Administration", "Database Development", "Database Programming", "Debian",
                "Debugging", "Delphi", "Django", "DNS", "DOS", "DotNetNuke", "Drupal", "Dynamics", "eCommerce", "edX",
                "Elasticsearch", "eLearning", "Electronic Forms", "Embedded Software", "Erlang", "Estonian",
                "Express JS", "Expression Engine", "Face Recognition", "FileMaker", "Firefox", "Fortran",
                "Forum Software", "FreelancerAPI", "Game Consoles", "Game Design", "Game Development", "Gamification",
                "Google Analytics", "Google App Engine", "Google Chrome", "Google Earth", "Google Go", "Google Plus",
                "Google Web Toolkit", "Google Webmaster Tools", "Google Website Optimizer", "GPGPU", "Grease Monkey",
                "Hadoop", "Haskell", "HP Openview", "HTML", "HTML5", "IBM Tivoli", "IBM Websphere Transformation T",
                "IIS", "Internet Security", "Interspire", "J2EE", "Jabber", "Java", "JavaFX", "Javascript", "Joomla",
                "jQuery / Prototype", "JSP", "Kinect", "Knockout.js", "LabVIEW", "Laravel", "Link Building", "Linkedin",
                "Linux", "Lisp", "Lotus Notes", "Mac OS", "Magento", "Map Reduce", "MariaDB", "Metatrader", "Microsoft",
                "Microsoft Access", "Microsoft Exchange", "Microsoft Expression", "Microsoft SQL Server", "MMORPG",
                "Mobile App Testing", "MODx", "Moodle", "MVC", "MySpace", "MySQL", "Network Administration", "Nginx",
                "Ning", "node.js", "NoSQL Couch & Mongo", "Objective C", "OCR", "Open Cart", "OpenCL", "OpenGL",
                "Oracle", "OSCommerce", "Parallels Automation", "Parallels Desktop", "Pattern Matching", "Paypal API",
                "Pentaho", "Perl", "PhoneGap", "Photoshop Coding", "PHP", "PICK Multivalue DB", "Pinterest", "Plesk",
                "PostgreSQL", "Prestashop", "Prolog", "Protoshare", "Puppet", "Python", "QlikView", "QuickBase",
                "R Programming Language", "REALbasic", "Red Hat", "Redis", "RESTful", "Rocket Engine", "Ruby",
                "Ruby on Rails", "Salesforce App Development", "SAP", "Scala", "Scheme", "Script Install",
                "Scrum Development", "Sencha / YahooUI", "SEO", "Sharepoint", "Shell Script", "Shopify",
                "Shopping Carts", "Siebel", "Silverlight", "Smarty PHP", "Social Engine", "Social Networking",
                "Socket IO", "Software Architecture", "Software Development", "Software Testing", "Solaris", "SQL",
                "SQLite", "SugarCRM", "Swift", "Symfony PHP", "System Admin", "TaoBao API", "TestStand", "Tumblr",
                "Twitter", "TYPO3", "Ubuntu", "Umbraco", "UML Design", "Unity 3D", "UNIX", "Usability Testing",
                "User Interface / IA", "VB.NET", "vBulletin", "VertexFX", "Virtual Worlds", "Virtuemart", "Virtuozzo",
                "Visual Basic", "Visual Basic for Apps", "Visual Foxpro", "VMware", "VoiceXML", "VoIP", "Volusion",
                "VPS", "vTiger", "Web Hosting", "Web Scraping", "Web Security", "Web Services", "webMethods",
                "Website Management", "Website Testing", "WHMCS", "Windows 8", "Windows API", "Windows Desktop",
                "Windows Server", "Wordpress", "WPF", "x86/x64 Assembler", "XML", "XMPP", "XQuery", "XSLT", "Yii",
                "YouTube", "Zen Cart", "Zend", "Zoho", "Amazon Fire", "Amazon Kindle", "Android", "Android Honeycomb",
                "Appcelerator Titanium", "Blackberry", "Geolocation", "iPad", "iPhone", "J2ME", "Metro", "Mobile Phone",
                "Nokia", "Palm", "Samsung", "Symbian", "WebOS", "Windows CE", "Windows Mobile", "Windows Phone",
                "3D Animation", "3D Design", "3D Modelling", "3D Rendering", "3ds Max", "ActionScript",
                "Adobe Dreamweaver", "Adobe Flash", "Adobe InDesign", "Adobe LiveCycle Designer", "Advertisement Design",
                "After Effects", "Animation", "Apple Compressor", "Apple Logic Pro", "Apple Motion", "Arts & Crafts",
                "Audio Production", "Audio Services", "Autodesk Revit", "Banner Design", "Blog Design", "Bootstrap",
                "Brochure Design", "Building Architecture", "Business Cards", "Capture NX2", "Caricature & Cartoons",
                "CGI", "Cinema 4D", "Commercials", "Concept Design", "Corporate Identity", "Covers & Packaging",
                "Creative Design", "CSS", "Fashion Design", "Fashion Modeling", "Final Cut Pro", "Finale / Sibelius",
                "Flash 3D", "Flex", "Flyer Design", "Format & Layout", "Furniture Design", "GarageBand",
                "Google SketchUp", "Graphic Design", "Icon Design", "Illustration", "Illustrator", "iMovie",
                "Industrial Design", "Infographics", "Interior Design", "Invitation Design", "JDF", "Label Design",
                "Landing Pages", "Logo Design", "Makerbot", "Maya", "Motion Graphics", "Music", "Package Design",
                "Photo Editing", "Photography", "Photoshop", "Photoshop Design", "Post-Production", "Poster Design",
                "Pre-production", "Presentations", "Prezi", "Print", "PSD to HTML", "PSD2CMS", "QuarkXPress", "RWD",
                "Shopify Templates", "Sound Design", "Stationery Design", "Sticker Design", "T-Shirts", "Templates",
                "Typography", "User Experience Design", "Video Broadcasting", "Video Editing", "Video Production",
                "Video Services", "Videography", "Visual Arts", "Voice Talent", "Website Design", "Word",
                "Yahoo! Store Design", "Algorithm", "Arduino", "Astrophysics", "AutoCAD", "Biology", "Biotechnology",
                "CAD/CAM", "Circuit Design", "Clean Technology", "Climate Sciences", "Construction Monitoring",
                "Cryptography", "Data Mining", "Digital Design", "Drones", "Engineering", "Engineering Drawing",
                "Finite Element Analysis", "Genetic Engineering", "Geology", "Geospatial", "GPS", "Home Design",
                "Human Sciences", "Imaging", "Linear Programming", "Machine Learning", "Manufacturing Design",
                "Mathematics", "Matlab & Mathematica", "Mechatronics", "Medical", "Microcontroller", "Microstation",
                "Nanotechnology", "Natural Language", "PCB Layout", "Physics", "PLC & SCADA", "Product Management",
                "Project Scheduling", "Quantum", "Remote Sensing", "Renewable Energy Design", "Robotics", "RTOS",
                "Scientific Research", "Solidworks", "Statistical Analysis", "Statistics", "Verilog / VHDL", "Wireless",
                "Wolfram", "Testing", "Product development ", "Hibernet", "Spring", "JSON", "LAMP", "Flash", "JVM",
                "Mongo", "Memcache", "Database Structure", "Project Management", "Framework", "ROR", "SSRS", "MSCRM",
                "WORKFLOW", "ASTERIX", "STREAMING", "SAAS", "STACK", "ERP", "Cluster", "IS", "CITRIX", "API", "CACHE",
                "CACHING", "LOAD BALANCING", "LOAD RUNNER", "UNIGRAPHICS", "KERNEL", "RDBMS", "JBOSS", "STRUTS",
                "SERVLET", "JDBC", "SOAP", "CORE JAVA", "IOS", "MOBILE GAME DEVELOPER", "DEVELOPER", "VIRTULIZATION",
                "Prototype", "TCP/UDP", "OOPS", "Multithreading", "UI Developer", "Adobe Photoshop", "Adobe Illustrator",
                "Tomcat", "Maven", "Selenium", "Backend API", "Eclipse", "Data Structure", "NOSQL", "JENKINS",
                "Mercurial", "Junit", "SVN / Subversion", "Ant", "Release", "MapReduce 2.0/ MRv2 / YARN", "HBase",
                "SPARK", "MS Dynamics-AX", "2D", "2G", "3G", "4G", "ABAP", "AirWatch", "AIX", "Alfresco",
                "Analog Layout", "Apex", "Application Packaging", "ARM", "Artificial Intelligence", "AS400", "ASIC",
                "Asterisk", "ATE", "ATG", "Avaya", "Avionics", "AWS", "AX", "AXAPTA", "BI", "BIST", "Black Box Testing",
                "Bluetooth", "BMC", "Board Design", "BOXI", "BRM", "BSP", "BSS", "Build & Release", "CA Tools", "CAD",
                "Cadence Allegro", "Cadence Virtuoso", "CAE", "Calypso", "CAM", "Cassandra", "CATIA", "CCNA", "CDMA",
                "CDN", "Chipset", "Clock Tree Synthesis", "CMOS", "Codec", "CodeIgnite", "Cognos", "Computer Vision",
                "Control-M", "CQ", "CQ5", "Crystal Reports", "CSS3", "CyberArk", "DB2", "DBA", "Device Driver", "DevOps",
                "DFT", "DICOM", "DOJO", "Dreamweaver", "Drools", "DSP", "Duck Creek", "EAI", "ECM", "EDA", "EDI", "eGRC",
                "EJB", "Ember.js", "EMC", "EMS", "Enovia", "EPM", "ESB", "Essbase", "Ethical Hacking", "Excel",
                "Exchange", "ExtJS", "Facets", "Filenet", "Firewall", "Firmware", "Flexcube", "Floorplan", "FPGA",
                "Full Stack", "Genesys", "GIS", "Google Maps", "GPRS", "greenplum", "Groovy/Grails", "GSM", "GUI",
                "Guidewire", "GWT", "HCM", "Hive", "HMI", "HP Exstream", "HP PPM", "HP-UX", "HRMS", "HTTP", "Hybris",
                "Hyper-V", "iAM", "IC Compiler", "IDM", "IEEE", "iLog", "Image Processing", "IMS", "Incident Management",
                "Indesign", "InfoPath", "Informatica", "Informix", "InfoSphere", "IO", "IPv6", "IT Compliance",
                "IT Research", "IT Service Delivery", "IVR", "Jasper", "JCL", "JDEdwards", "Jira", "Jive", "JMeter",
                "jQuery", "JS", "JSF", "Juniper", "Kofax", "Kronos", "Liferay", "LINQ", "Lombardi", "LTE", "Lucene",
                "Lync", "Magma Talus", "Mainframe", "Manual Testing", "MATLAB", "MCAL", "MDM", "MDX", "Medical Imaging",
                "Memory Characterization", "Memory Design", "Memory Management", "Message Broker", "MFC",
                "Microstrategy", "Middleware", "Mixed Signal", "Mobile", "Moss", "MPEG", "MQ", "MS CRM", "MS Dynamics",
                "MS SQL", "Murex", "navision", "NetApp", "Netcool", "Netezza", "NetSuite", "Netweaver", "Nimsoft", "NLP",
                "NMS", "Nonrel", "Nutch", "OAM", "OBIA", "OBIEE", "ODI", "OFSAA", "OIM", "OLAP", "OMS", "OOAD",
                "Open Pages", "Open Stack", "OpenCV", "OpenLink", "OpenStack", "OpenText", "OPM", "Optical Networking",
                "Oracle ADF", "Oracle APEX", "Oracle Apps", "Oracle ASAP", "Oracle ASCP", "Oracle BI", "Oracle BPM",
                "Oracle BRM", "Oracle DB", "Oracle Demantra", "Oracle EBS", "Oracle Forms", "Oracle Fusion",
                "Oracle HCM", "Oracle HR", "Oracle HRMS", "Oracle IAM", "Oracle IDM", "Oracle O2C", "Oracle OAF",
                "Oracle OM", "Oracle OSB", "Oracle RAC", "Oracle SCM", "Oracle SOA", "OSM", "OSS", "OTM", "OVM",
                "Payment Gateway", "PCB", "Pega", "Peoplesoft FSCM", "PeopleSoft HCM", "Physical Design", "PL/1",
                "PL/SQL", "PLM", "PL-SQL", "Powerbuilder", "PowerShell", "Primavera", "Prince2", "Pro*C",
                "Program Management", "Progress", "Progress 4GL", "PRPC", "QAD", "QRadar", "QTP", "R12", "RF Design",
                "RPG", "RSA Archer", "RTL", "RTL Coding", "RTL Design", "SailPoint", "Salesforce", "SAP BODS", "SAP BPM",
                "SAP Business One", "SAP CC", "SAP CIN", "SAP CO", "SAP CS", "SAP DBM", "SAP EP", "SAP ERP", "SAP ESS",
                "SAP FICA", "SAP FSCM", "SAP GTS", "SAP HANA", "SAP ISU", "SAP LE", "SAP MDM", "SAP P2P", "SAP PI",
                "SAP PLM", "SAP PPM", "SAP PS", "SAP Security", "SAP Testing", "SAP TM", "SAP UI5", "SCCM", "Schema",
                "Scripting", "Scrum", "SDK", "SDLC", "Security", "Sencha", "Sensor", "Server Administration",
                "ServiceNow", "Shell", "Shell Scripting", "Silk Testing", "Singleview", "SIP", "Sitecore", "SOA", "SoC",
                "SoC Encounter", "Solr", "Specman", "Speech Recognition", "Spotfire", "Springs", "SPSS", "SQL Server",
                "SSAS", "SSIS", "STA", "Standard Cell Layout", "Static Timing Analysis", "STB", "Sterling Commerce",
                "STL", "SuccessFactors", "Swing", "Sybase", "Syclo", "Synopsys", "Synthesis", "System Administartion",
                "System Verilog", "SystemC", "Tableau", "Talend", "Taleo", "TCL", "TCP/IP", "Teamcenter", "TeamSite",
                "Technical Architech", "Technical Support", "Technical Training", "Technical Writer", "Tekla", "Telecom",
                "Teradata", "Test Automation", "Tibco", "Timing Closure", "Titanium", "TIVOLI", "TM1", "T-SQL", "UAT",
                "UI", "UML", "Unilever", "Unity", "USB", "User Interface", "UVM", "UX", "VAS", "VB", "VC++", "Vera",
                "Verification", "Verilog", "Veritas Cluster", "VHDL", "Video", "Video Streamig", "VisionPLUS",
                "Visual Force", "Visual Studio", "VLSI", "VMM", "VxWorks", "Walkin", "WAS", "Waterfall", "WCF", "WCS",
                "Web Method", "WebCenter", "WebDynpro", "WebLogic", "Webservices", "WebSphere", "WFM", "WFT",
                "White Box Testing", "Win32", "Windchill", "WinForms", "Wintel", "Wireframe", "WLAN", "Workday", "X++",
                "x86", "Xcode", "XenApp", "XHTML", "XMS", "YUI", "ZigBee", "IC", "ADAS", "Infotainment", "Interface",
                "Clocking", "Linear Power", "DLP", "AC-DC Drives", "ADA", "Allegro", "Altium", "APFC", "AtMega", "BTS",
                "CENELEC", "CMMI", "DCS", "Digital circuit", "ECSS", "ECU", "Embedded C", "EMI", "HDI Design",
                "High speedRF", "Layout Design", "LDRA", "Mixed Signal Design", "Modulation", "Networking", "PIC",
                "Power Electronics Design", "PSPICE", "Rectification", "RTCA", "RTRT", "Satellite AIT", "SCADA",
                "Semiconductor", "TSM", "Vector CAST", "VFD", "WebRTC", "Adaptability", "Analysis", "Awareness",
                "Business Management", "Calculation", "Certification", "Communication", "Computer", "Creative Thinking",
                "Critical Thinking", "Design", "Design Experiments", "Design Systems", "Develop", "Development",
                "Discipline", "Engineered", "Ethics", "Fabricated", "Generating Valuable Data", "Hardware",
                "Implementation", "Industry Codes", "Industry Standards", "Innovation", "Intellectual Ability",
                "Interpreting Data", "Investigate", "Knowledge", "Laboratory Work", "Logic", "Maintenance",
                "Manage Resources", "Managerial Techniques", "Manufacturing", "Materials", "Modification", "Monitoring",
                "Performance", "Practicality", "Problem Solving", "Processes", "Production", "Promote Safety Systems",
                "Responsibility", "Scheduling", "Science", "Software", "Specialist Knowledge", "Systematized", "Systems",
                "Systems Design", "Systems Evaluation", "Teamwork", "Technical", "Technological Tools", "Theory",
                "Time Management", "Transferable Skills", "Troubleshooting", "Understanding of External Constraints",
                "Undertake Lifelong Learning", "Use Information Technology Effectively", "Validation", "Value",
                "Work in a Multi-Disciplinary Team", "Workshop Equipment", "Hibernate", "Automation Testing",
                "ETL testing", "Performance Testing", "QTP Testing", "Protocol Testing", "System Testing",
                "Compliance Testing", "Load Testing", "Process Testing", "SIP Testing", "Selenium Testing",
                "Stress Testing", "Code Testing", "Failure/Bug Testing", "Game Testing", "Hardware Testing",
                "Verification Testing", "Validation Testing"
            };
            if (!_systemService.Skills.Any())
            {
                foreach (var x in skillText)
                {
                    _systemService.Create(new SkillDictionaryCreateViewModel
                    {
                        Code = pattern.Replace(x.ToLower(), ""),
                        Title = x
                    });
                }
            }

            var _countries = new[] { new CountryData { Code = "AD", Title = "Andorra" }, new CountryData { Code = "AE", Title = "United Arab Emirates" }, new CountryData { Code = "AF", Title = "Afghanistan" }, new CountryData { Code = "AG", Title = "Antigua and Barbuda" }, new CountryData { Code = "AI", Title = "Anguilla" }, new CountryData { Code = "AL", Title = "Albania" }, new CountryData { Code = "AM", Title = "Armenia" }, new CountryData { Code = "AO", Title = "Angola" }, new CountryData { Code = "AQ", Title = "Antarctica" }, new CountryData { Code = "AR", Title = "Argentina" }, new CountryData { Code = "AS", Title = "American Samoa" }, new CountryData { Code = "AT", Title = "Austria" }, new CountryData { Code = "AU", Title = "Australia" }, new CountryData { Code = "AW", Title = "Aruba" }, new CountryData { Code = "AX", Title = "Åland" }, new CountryData { Code = "AZ", Title = "Azerbaijan" }, new CountryData { Code = "BA", Title = "Bosnia and Herzegovina" }, new CountryData { Code = "BB", Title = "Barbados" }, new CountryData { Code = "BD", Title = "Bangladesh" }, new CountryData { Code = "BE", Title = "Belgium" }, new CountryData { Code = "BF", Title = "Burkina Faso" }, new CountryData { Code = "BG", Title = "Bulgaria" }, new CountryData { Code = "BH", Title = "Bahrain" }, new CountryData { Code = "BI", Title = "Burundi" }, new CountryData { Code = "BJ", Title = "Benin" }, new CountryData { Code = "BL", Title = "Saint Barthélemy" }, new CountryData { Code = "BM", Title = "Bermuda" }, new CountryData { Code = "BN", Title = "Brunei" }, new CountryData { Code = "BO", Title = "Bolivia" }, new CountryData { Code = "BQ", Title = "Bonaire" }, new CountryData { Code = "BR", Title = "Brazil" }, new CountryData { Code = "BS", Title = "Bahamas" }, new CountryData { Code = "BT", Title = "Bhutan" }, new CountryData { Code = "BV", Title = "Bouvet Island" }, new CountryData { Code = "BW", Title = "Botswana" }, new CountryData { Code = "BY", Title = "Belarus" }, new CountryData { Code = "BZ", Title = "Belize" }, new CountryData { Code = "CA", Title = "Canada" }, new CountryData { Code = "CC", Title = "Cocos [Keeling] Islands" }, new CountryData { Code = "CD", Title = "Democratic Republic of the Congo" }, new CountryData { Code = "CF", Title = "Central African Republic" }, new CountryData { Code = "CG", Title = "Republic of the Congo" }, new CountryData { Code = "CH", Title = "Switzerland" }, new CountryData { Code = "CI", Title = "Ivory Coast" }, new CountryData { Code = "CK", Title = "Cook Islands" }, new CountryData { Code = "CL", Title = "Chile" }, new CountryData { Code = "CM", Title = "Cameroon" }, new CountryData { Code = "CN", Title = "China" }, new CountryData { Code = "CO", Title = "Colombia" }, new CountryData { Code = "CR", Title = "Costa Rica" }, new CountryData { Code = "CU", Title = "Cuba" }, new CountryData { Code = "CV", Title = "Cape Verde" }, new CountryData { Code = "CW", Title = "Curacao" }, new CountryData { Code = "CX", Title = "Christmas Island" }, new CountryData { Code = "CY", Title = "Cyprus" }, new CountryData { Code = "CZ", Title = "Czech Republic" }, new CountryData { Code = "DE", Title = "Germany" }, new CountryData { Code = "DJ", Title = "Djibouti" }, new CountryData { Code = "DK", Title = "Denmark" }, new CountryData { Code = "DM", Title = "Dominica" }, new CountryData { Code = "DO", Title = "Dominican Republic" }, new CountryData { Code = "DZ", Title = "Algeria" }, new CountryData { Code = "EC", Title = "Ecuador" }, new CountryData { Code = "EE", Title = "Estonia" }, new CountryData { Code = "EG", Title = "Egypt" }, new CountryData { Code = "EH", Title = "Western Sahara" }, new CountryData { Code = "ER", Title = "Eritrea" }, new CountryData { Code = "ES", Title = "Spain" }, new CountryData { Code = "ET", Title = "Ethiopia" }, new CountryData { Code = "FI", Title = "Finland" }, new CountryData { Code = "FJ", Title = "Fiji" }, new CountryData { Code = "FK", Title = "Falkland Islands" }, new CountryData { Code = "FM", Title = "Micronesia" }, new CountryData { Code = "FO", Title = "Faroe Islands" }, new CountryData { Code = "FR", Title = "France" }, new CountryData { Code = "GA", Title = "Gabon" }, new CountryData { Code = "GB", Title = "United Kingdom" }, new CountryData { Code = "GD", Title = "Grenada" }, new CountryData { Code = "GE", Title = "Georgia" }, new CountryData { Code = "GF", Title = "French Guiana" }, new CountryData { Code = "GG", Title = "Guernsey" }, new CountryData { Code = "GH", Title = "Ghana" }, new CountryData { Code = "GI", Title = "Gibraltar" }, new CountryData { Code = "GL", Title = "Greenland" }, new CountryData { Code = "GM", Title = "Gambia" }, new CountryData { Code = "GN", Title = "Guinea" }, new CountryData { Code = "GP", Title = "Guadeloupe" }, new CountryData { Code = "GQ", Title = "Equatorial Guinea" }, new CountryData { Code = "GR", Title = "Greece" }, new CountryData { Code = "GS", Title = "South Georgia and the South Sandwich Islands" }, new CountryData { Code = "GT", Title = "Guatemala" }, new CountryData { Code = "GU", Title = "Guam" }, new CountryData { Code = "GW", Title = "Guinea-Bissau" }, new CountryData { Code = "GY", Title = "Guyana" }, new CountryData { Code = "HK", Title = "Hong Kong" }, new CountryData { Code = "HM", Title = "Heard Island and McDonald Islands" }, new CountryData { Code = "HN", Title = "Honduras" }, new CountryData { Code = "HR", Title = "Croatia" }, new CountryData { Code = "HT", Title = "Haiti" }, new CountryData { Code = "HU", Title = "Hungary" }, new CountryData { Code = "ID", Title = "Indonesia" }, new CountryData { Code = "IE", Title = "Ireland" }, new CountryData { Code = "IL", Title = "Israel" }, new CountryData { Code = "IM", Title = "Isle of Man" }, new CountryData { Code = "IN", Title = "India" }, new CountryData { Code = "IO", Title = "British Indian Ocean Territory" }, new CountryData { Code = "IQ", Title = "Iraq" }, new CountryData { Code = "IR", Title = "Iran" }, new CountryData { Code = "IS", Title = "Iceland" }, new CountryData { Code = "IT", Title = "Italy" }, new CountryData { Code = "JE", Title = "Jersey" }, new CountryData { Code = "JM", Title = "Jamaica" }, new CountryData { Code = "JO", Title = "Jordan" }, new CountryData { Code = "JP", Title = "Japan" }, new CountryData { Code = "KE", Title = "Kenya" }, new CountryData { Code = "KG", Title = "Kyrgyzstan" }, new CountryData { Code = "KH", Title = "Cambodia" }, new CountryData { Code = "KI", Title = "Kiribati" }, new CountryData { Code = "KM", Title = "Comoros" }, new CountryData { Code = "KN", Title = "Saint Kitts and Nevis" }, new CountryData { Code = "KP", Title = "North Korea" }, new CountryData { Code = "KR", Title = "South Korea" }, new CountryData { Code = "KW", Title = "Kuwait" }, new CountryData { Code = "KY", Title = "Cayman Islands" }, new CountryData { Code = "KZ", Title = "Kazakhstan" }, new CountryData { Code = "LA", Title = "Laos" }, new CountryData { Code = "LB", Title = "Lebanon" }, new CountryData { Code = "LC", Title = "Saint Lucia" }, new CountryData { Code = "LI", Title = "Liechtenstein" }, new CountryData { Code = "LK", Title = "Sri Lanka" }, new CountryData { Code = "LR", Title = "Liberia" }, new CountryData { Code = "LS", Title = "Lesotho" }, new CountryData { Code = "LT", Title = "Lithuania" }, new CountryData { Code = "LU", Title = "Luxembourg" }, new CountryData { Code = "LV", Title = "Latvia" }, new CountryData { Code = "LY", Title = "Libya" }, new CountryData { Code = "MA", Title = "Morocco" }, new CountryData { Code = "MC", Title = "Monaco" }, new CountryData { Code = "MD", Title = "Moldova" }, new CountryData { Code = "ME", Title = "Montenegro" }, new CountryData { Code = "MF", Title = "Saint Martin" }, new CountryData { Code = "MG", Title = "Madagascar" }, new CountryData { Code = "MH", Title = "Marshall Islands" }, new CountryData { Code = "MK", Title = "Macedonia" }, new CountryData { Code = "ML", Title = "Mali" }, new CountryData { Code = "MM", Title = "Myanmar [Burma]" }, new CountryData { Code = "MN", Title = "Mongolia" }, new CountryData { Code = "MO", Title = "Macao" }, new CountryData { Code = "MP", Title = "Northern Mariana Islands" }, new CountryData { Code = "MQ", Title = "Martinique" }, new CountryData { Code = "MR", Title = "Mauritania" }, new CountryData { Code = "MS", Title = "Montserrat" }, new CountryData { Code = "MT", Title = "Malta" }, new CountryData { Code = "MU", Title = "Mauritius" }, new CountryData { Code = "MV", Title = "Maldives" }, new CountryData { Code = "MW", Title = "Malawi" }, new CountryData { Code = "MX", Title = "Mexico" }, new CountryData { Code = "MY", Title = "Malaysia" }, new CountryData { Code = "MZ", Title = "Mozambique" }, new CountryData { Code = "NA", Title = "Namibia" }, new CountryData { Code = "NC", Title = "New Caledonia" }, new CountryData { Code = "NE", Title = "Niger" }, new CountryData { Code = "NF", Title = "Norfolk Island" }, new CountryData { Code = "NG", Title = "Nigeria" }, new CountryData { Code = "NI", Title = "Nicaragua" }, new CountryData { Code = "NL", Title = "Netherlands" }, new CountryData { Code = "NO", Title = "Norway" }, new CountryData { Code = "NP", Title = "Nepal" }, new CountryData { Code = "NR", Title = "Nauru" }, new CountryData { Code = "NU", Title = "Niue" }, new CountryData { Code = "NZ", Title = "New Zealand" }, new CountryData { Code = "OM", Title = "Oman" }, new CountryData { Code = "PA", Title = "Panama" }, new CountryData { Code = "PE", Title = "Peru" }, new CountryData { Code = "PF", Title = "French Polynesia" }, new CountryData { Code = "PG", Title = "Papua New Guinea" }, new CountryData { Code = "PH", Title = "Philippines" }, new CountryData { Code = "PK", Title = "Pakistan" }, new CountryData { Code = "PL", Title = "Poland" }, new CountryData { Code = "PM", Title = "Saint Pierre and Miquelon" }, new CountryData { Code = "PN", Title = "Pitcairn Islands" }, new CountryData { Code = "PR", Title = "Puerto Rico" }, new CountryData { Code = "PS", Title = "Palestine" }, new CountryData { Code = "PT", Title = "Portugal" }, new CountryData { Code = "PW", Title = "Palau" }, new CountryData { Code = "PY", Title = "Paraguay" }, new CountryData { Code = "QA", Title = "Qatar" }, new CountryData { Code = "RE", Title = "Réunion" }, new CountryData { Code = "RO", Title = "Romania" }, new CountryData { Code = "RS", Title = "Serbia" }, new CountryData { Code = "RU", Title = "Russia" }, new CountryData { Code = "RW", Title = "Rwanda" }, new CountryData { Code = "SA", Title = "Saudi Arabia" }, new CountryData { Code = "SB", Title = "Solomon Islands" }, new CountryData { Code = "SC", Title = "Seychelles" }, new CountryData { Code = "SD", Title = "Sudan" }, new CountryData { Code = "SE", Title = "Sweden" }, new CountryData { Code = "SG", Title = "Singapore" }, new CountryData { Code = "SH", Title = "Saint Helena" }, new CountryData { Code = "SI", Title = "Slovenia" }, new CountryData { Code = "SJ", Title = "Svalbard and Jan Mayen" }, new CountryData { Code = "SK", Title = "Slovakia" }, new CountryData { Code = "SL", Title = "Sierra Leone" }, new CountryData { Code = "SM", Title = "San Marino" }, new CountryData { Code = "SN", Title = "Senegal" }, new CountryData { Code = "SO", Title = "Somalia" }, new CountryData { Code = "SR", Title = "Suriname" }, new CountryData { Code = "SS", Title = "South Sudan" }, new CountryData { Code = "ST", Title = "São Tomé and Príncipe" }, new CountryData { Code = "SV", Title = "El Salvador" }, new CountryData { Code = "SX", Title = "Sint Maarten" }, new CountryData { Code = "SY", Title = "Syria" }, new CountryData { Code = "SZ", Title = "Swaziland" }, new CountryData { Code = "TC", Title = "Turks and Caicos Islands" }, new CountryData { Code = "TD", Title = "Chad" }, new CountryData { Code = "TF", Title = "French Southern Territories" }, new CountryData { Code = "TG", Title = "Togo" }, new CountryData { Code = "TH", Title = "Thailand" }, new CountryData { Code = "TJ", Title = "Tajikistan" }, new CountryData { Code = "TK", Title = "Tokelau" }, new CountryData { Code = "TL", Title = "East Timor" }, new CountryData { Code = "TM", Title = "Turkmenistan" }, new CountryData { Code = "TN", Title = "Tunisia" }, new CountryData { Code = "TO", Title = "Tonga" }, new CountryData { Code = "TR", Title = "Turkey" }, new CountryData { Code = "TT", Title = "Trinidad and Tobago" }, new CountryData { Code = "TV", Title = "Tuvalu" }, new CountryData { Code = "TW", Title = "Taiwan" }, new CountryData { Code = "TZ", Title = "Tanzania" }, new CountryData { Code = "UA", Title = "Ukraine" }, new CountryData { Code = "UG", Title = "Uganda" }, new CountryData { Code = "UM", Title = "U.S. Minor Outlying Islands" }, new CountryData { Code = "US", Title = "United States" }, new CountryData { Code = "UY", Title = "Uruguay" }, new CountryData { Code = "UZ", Title = "Uzbekistan" }, new CountryData { Code = "VA", Title = "Vatican City" }, new CountryData { Code = "VC", Title = "Saint Vincent and the Grenadines" }, new CountryData { Code = "VE", Title = "Venezuela" }, new CountryData { Code = "VG", Title = "British Virgin Islands" }, new CountryData { Code = "VI", Title = "U.S. Virgin Islands" }, new CountryData { Code = "VN", Title = "Vietnam" }, new CountryData { Code = "VU", Title = "Vanuatu" }, new CountryData { Code = "WF", Title = "Wallis and Futuna" }, new CountryData { Code = "WS", Title = "Samoa" }, new CountryData { Code = "XK", Title = "Kosovo" }, new CountryData { Code = "YE", Title = "Yemen" }, new CountryData { Code = "YT", Title = "Mayotte" }, new CountryData { Code = "ZA", Title = "South Africa" }, new CountryData { Code = "ZM", Title = "Zambia" }, new CountryData { Code = "ZW", Title = "Zimbabwe" } };

            if (!_systemService.Countries.Any())
            {
                foreach (var x in _countries)
                {
                    _systemService.Create(new SkillDictionaryCreateViewModel
                    {
                        Code = x.Code,
                        Title = x.Title
                    });
                }
            }

        }

        public async Task<string> BuildAsync()
        {
            var skills = _systemService.Skills.ToArray();
            var areas = _systemService.FunctionalAreas.ToArray();
            var industries = _systemService.Industries.ToArray();

            var skillFactory = AutoPocoContainer.Configure(x =>
            {
                x.Conventions(c => { c.UseDefaultConventions(); });
                x.AddFromAssemblyContainingType<ContractorSkillViewModel>();
                x.Include<ContractorSkillViewModel>()
                    .Setup(u => u.ExperienceInMonths).Use<IntegerSource>(1, 60 * 12)
                    .Setup(u => u.Level).Use<EnumSource<LevelEnum>>()
                    .Setup(u => u.Proficiency).Use<EnumSource<ProficiencyEnum>>()
                    .Setup(u => u.Title).Use<DictionaryCodeSource>(new[] { skills });
            });

            var userFactory = AutoPocoContainer.Configure(x =>
            {
                x.Conventions(c => { c.UseDefaultConventions(); });
                x.AddFromAssemblyContainingType<ContractorCreateViewModel>();
                x.Include<ContractorCreateViewModel>()
                    .Setup(u => u.Email).Use<EmailAddressSource>()
                    .Setup(u => u.FirstName).Use<GangFirstNameSource>()
                    .Setup(u => u.LastName).Use<GangLastNameSource>()
                    .Setup(u => u.About).Use<LoremIpsumSource>(2)
                    .Setup(u => u.FunctionalAreaId).Use<DictionarySource>(new[] { areas })
                    .Setup(u => u.IndustryId).Use<DictionarySource>(new[] { industries })
                    .Setup(u => u.ConsultantType).Use<EnumSource<ContractorTypeEnum>>()
                    .Setup(u => u.Gender).Use<EnumSource<GenderEnum>>()
                    .Setup(u => u.Rate).Use<IntegerSource>(100, 10000)
                    .Setup(u => u.ExperienceMonths).Use<IntegerSource>(1, 11)
                    .Setup(u => u.ExperienceYears).Use<IntegerSource>(1, 60)
                    .Setup(u => u.Location).Use<IndianCitySource>()
                    .Setup(u => u.Nationality).Use<CountrySource>()
                    .Setup(u => u.Profile).Use<LoremIpsumSource>(20)
                    .Setup(u => u.AlternateNumber).Use<DutchTelephoneSource>()
                    .Setup(u => u.Mobile).Use<DutchTelephoneSource>()
                    .Setup(u => u.RateType).Use<EnumSource<RateEnum>>()
                    .Setup(u => u.ContractType).Use<EnumSource<ContractTypeEnum>>();
            });

            var scheduleFactory = AutoPocoContainer.Configure(x =>
            {
                x.Conventions(c => { c.UseDefaultConventions(); });
                x.AddFromAssemblyContainingType<CreateScheduleViewModel>();
                x.Include<CreateScheduleViewModel>()
                    .Setup(c => c.Start).Use<DateTimeSource>(DateTime.UtcNow.AddMonths(-1), DateTime.UtcNow.AddMonths(10))
                    .Setup(c => c.End).Use<DateTimeNullableSource>(DateTime.UtcNow.AddMonths(-1), DateTime.UtcNow.AddMonths(10))
                    .Setup(c => c.Company).Use<CompanySource>()
                    .Setup(c => c.IsAvailable).Use<BooleanSource>();
            });

            var users = userFactory.CreateSession().List<ContractorCreateViewModel>(100).Get();
            var skillSession = skillFactory.CreateSession();
            var scheduleSession = scheduleFactory.CreateSession();

            foreach (var u in users)
            {
                u.Skills = skillSession.List<ContractorSkillViewModel>(_random.Next(6, 20)).Get();
                u.OwnerId = await _userService.CreateAsync(u.Email, "Abcd123*", _contractor);
                var ct = _contractorService.Create(u);

                var schedules = scheduleSession.List<CreateScheduleViewModel>(_random.Next(3, 10)).Get();
                foreach (var schedule in schedules)
                {
                    var tmp = schedule.End ?? DateTime.UtcNow.AddYears(20);
                    if (schedule.Start > tmp)
                    {
                        schedule.End = schedule.Start;
                        schedule.Start = tmp;
                    }
                    _contractorService.Create(schedule, ct.Id);
                }
            }

            var companyFactory = AutoPocoContainer.Configure(x =>
            {
                x.Conventions(c => c.UseDefaultConventions());
                x.AddFromAssemblyContainingType<CompanyCreateViewModel>();
                x.Include<CompanyCreateViewModel>()
                    .Setup(c => c.CompanyName).Use<CompanySource>()
                    .Setup(c => c.FirstName).Use<FirstNameSource>()
                    .Setup(c => c.LastName).Use<LastNameSource>()
                    .Setup(c => c.Email).Use<ExtendedEmailAddressSource>()
                    .Setup(c => c.Location).Use<IndianCitySource>()
                    .Setup(u => u.AlternateNumber).Use<DutchTelephoneSource>()
                    .Setup(u => u.Mobile).Use<DutchTelephoneSource>()
                    .Setup(c => c.About).Use<LoremIpsumSource>(2)
                    .Setup(c => c.IndustryId).Use<DictionarySource>(new[] { industries })
                    .Setup(c => c.OrganizationType).Use<EnumSource<OrganizationTypeEnum>>()
                    .Setup(c => c.WebSite).Use<UrlSource>();
            });

            var companies = companyFactory.CreateSession().List<CompanyCreateViewModel>(25).Get();

            var jobFactory = AutoPocoContainer.Configure(x =>
            {
                x.Conventions(c => c.UseDefaultConventions());
                x.AddFromAssemblyContainingType<CreateJobViewModel>();
                x.Include<CreateJobViewModel>()
                    .Setup(c => c.Description).Use<LoremIpsumSource>(20)
                    .Setup(c => c.Title).Use<JobTitleSource>()
                    //.Setup(c => c.Location).Use<IndianCitySource>()
                    .Setup(c => c.Rate).Use<IntegerSource>(100, 10000)
                    .Setup(c => c.Start).Use<DateTimeSource>(DateTime.UtcNow.AddMonths(-1), DateTime.UtcNow.AddMonths(10))
                    .Setup(c => c.End).Use<DateTimeSource>(DateTime.UtcNow.AddMonths(3), DateTime.UtcNow.AddMonths(36));
            });

            var jobSkillFactory = AutoPocoContainer.Configure(x =>
            {
                x.Conventions(c => { c.UseDefaultConventions(); });
                x.AddFromAssemblyContainingType<JobSkillEditViewModel>();
                x.Include<JobSkillEditViewModel>()
                    .Setup(u => u.Level).Use<EnumSource<LevelEnum>>()
                    .Setup(u => u.Title).Use<DictionaryCodeSource>(new[] { skills });
            });

            var jobSession = jobFactory.CreateSession();
            var jobSkillSession = jobSkillFactory.CreateSession();

            foreach (var u in companies)
            {
                u.OwnerId = await _userService.CreateAsync(u.Email, "Abcd123*", _company);
                _companyService.CurrentUserId = u.OwnerId;

                var company = _companyService.Create(u);
                var jobs = jobSession.List<CreateJobViewModel>(_random.Next(5, 25)).Get(); ;
                foreach (var job in jobs)
                {
                    if (job.Start > job.End)
                    {
                        var tmp = job.End;
                        job.End = job.Start;
                        job.Start = tmp;
                    }
                    job.Skills = jobSkillSession.List<JobSkillEditViewModel>(_random.Next(5, 20)).Get(); ;
                    var jb = _companyService.Create(job, company.Id);

                    if (_random.Next(1, 3) == 2)
                    {
                        _companyService.Publish(new PublishJobViewModel()
                        {
                            Id = jb.Id
                        });
                    }

                    if (_random.Next(1, 5) == 2)
                    {
                        _companyService.Cancel(new CancelJobViewModel()
                        {
                            Id = jb.Id
                        });
                    }
                }
            }

            return "All Done";
        }
    }
}