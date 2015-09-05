app.factory('dataService', ['$http', '$q', 'contractorService', 'companyService', 'billingService', 'jobService', 'systemService',
    function ($http, $q, contractorService, companyService, billingService, jobService, systemService) {

    return {
        pageSize: 10,
        contractor: contractorService,
        job:jobService,
        company: companyService,
        billing:billingService,
        system: systemService,
        role : document.querySelector('html').dataset.role,
        args:findArguments
    };

    function findArguments() {
        var tmp = {};
        var args = window.location.hash.split('?');
        if (args.length > 1) {
            var rgs = args[1].split("&");
            for(var x in rgs) {
                var k = rgs[x].split('=');
                tmp[k[0]] = k.length > 1 ? k[1] : null;
            }
        }
        return tmp;
    }
}]);