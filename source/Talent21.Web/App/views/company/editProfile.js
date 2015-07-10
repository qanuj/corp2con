app.controller('companyEditProfileController', ['$scope', 'dataService', function ($scope, db) {

    db.company.profile().success(function (result) {
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
        db.company.editProfile(record)
            .success(function (result) {
                console.log(result);
            });
    }
}]);