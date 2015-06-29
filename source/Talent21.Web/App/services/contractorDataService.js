app.factory('contractorDataService', ['$http', function($http) {
    var factory = {};

    factory.variable = 'Abcde';

    factory.getAllContractors = function() {
        $http.get('api/v1/candidate/')
            .success(function(data, status, headers, config) {
                var allContractors = data;
                console.log(allContractors);
            })
            .error(function(data, status, headers, config) {
                console.log(status);
            });
    }

    console.log(factory);
    return factory;
}]);