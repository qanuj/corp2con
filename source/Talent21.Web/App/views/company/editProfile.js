app.controller('companyEditProfileController', ['$scope', 'dataService', function ($scope, db) {

    db.company.profile().success(function (result) {
        $scope.record = result;
    });

    $scope.save = function (record) {
        db.company.editProfile(record).success(function (result) {
            console.log(result);
        });
    }
}]);