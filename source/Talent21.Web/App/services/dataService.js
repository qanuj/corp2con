app.factory('dataService', ['$http', function ($http) {
    var v = 'api/v1/';
    var factory = {
        contractor: {},
        job: {},
        company: {},
        system: {}
    };

    factory.contractor.paged = function () {
        return $http.get(v + 'candidate/paged?$inlinecount=allpages');
    }

    factory.contractor.all = function () {
        return $http.get(v + 'candidate/all');
    }

    factory.contractor.profile = function () {
        return $http.get(v + 'candidate/profile');
    }

    return factory;
}]);