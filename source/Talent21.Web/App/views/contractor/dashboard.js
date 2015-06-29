app.controller('contractorDashboardController', ['$scope', function ($scope, contractorDataService, $http) {
    $scope.title = "Contractor Dashboard";
    $scope.returnContractors = function() {
        /*contractorDataService.getAllContractors();*/
        console.log(contractorDataService);
    }
}]);