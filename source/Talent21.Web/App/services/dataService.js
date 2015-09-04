app.factory('dataService', ['$http', '$q', 'contractorService', 'companyService', 'billingService', 'jobService', 'systemService',
    function ($http, $q, contractorService, companyService, billingService, jobService, systemService) {

    return {
        pageSize: 10,
        contractor: contractorService,
        job:jobService,
        company: companyService,
        billing:billingService,
        system: systemService,
        role : document.querySelector('html').dataset.role
    };

}]);