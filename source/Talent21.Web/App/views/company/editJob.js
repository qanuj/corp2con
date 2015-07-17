app.controller('companyJobEditController', ['$scope', 'dataService', '$routeParams', '$window', function ($scope, db, param, $window) {

    $scope.title = "Edit Job";
    db.company.job().success(function (result) {
        $scope.record = result;
    });

    db.system.getLocations().success(function (result) {
        $scope.cities = result;
    });

    db.system.getIndustries().success(function (result) {
        $scope.industries = result;
        console.log(result);
    });

    $scope.save = function (record) {
        db.company.editJob(record)
            .success(function (result) {
                console.log(result);
            });
    }
}]);