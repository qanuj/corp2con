app.factory('dataService', ['$http', '$q', 'contractorService', 'companyService', 'jobService', 'systemService',
    function ($http, $q, contractorService, companyService, jobService, systemService) {

    return {
        pageSize: 10,
        contractor: contractorService,
        job:jobService,
        company:companyService,
        system: systemService,
        role : document.querySelector('html').dataset.role
    };

}]);