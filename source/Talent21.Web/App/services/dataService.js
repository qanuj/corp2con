app.factory('dataService', ['$http', '$q', 'contractorService', 'companyService', 'billingService', 'jobService', 'systemService', 'adminService',
    function ($http, $q, contractorService, companyService, billingService, jobService, systemService, adminService) {

    var factory={
        pageSize: 10,
        contractor: contractorService,
        job:jobService,
        company: companyService,
        admin: adminService,
        billing:billingService,
        system: systemService,
        role : document.querySelector('html').dataset.role,
        args:findArguments
    };

    factory.me = factory[factory.role.toLowerCase()];

    return factory;

    function findArguments(sp) {
        var tmp = sp || {};
        var args = window.location.hash.split('?');
        if (args.length > 1) {
            var rgs = args[1].split("&");
            for(var x in rgs) {
                var k = rgs[x].split('=');
                tmp[k[0]] = k.length > 1 && k[1]!='undefined' ? decodeURIComponent(k[1]) : null;
            }
        }
        return tmp;
    }
}]);