app.factory('dataService', ['$http', function ($http) {
    var v = 'api/v1/';
    var factory = {
        contractor: {},
        job: {},
        company: {},
        system: {}
    };

    factory.currentPage = window.location.href;

    factory.contractor.paged = function () {
        return $http.get(v + 'candidate/paged?$inlinecount=allpages');
    }

    factory.contractor.all = function () {
        return $http.get(v + 'candidate/all');
    }

    factory.contractor.profile = function () {
        return $http.get(v + 'candidate/profile');
    }

    factory.company.paged = function () {
        return $http.get(v + 'company/paged?$inlinecount=allpages');
    }

    factory.company.all = function () {
        return $http.get(v + 'company/all');
    }

    factory.company.profile = function () {
        return $http.get(v + 'company/profile');
    }
    return factory;
}]);