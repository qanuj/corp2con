app.controller('contractorEditProfileController', ['$scope', 'dataService', function ($scope, db) {

    db.contractor.profile().success(function(result) {
        $scope.record = result;
        console.log(result);
    });

    $scope.save = function(record) {
        db.contractor.editProfile(record).success(function (result) {
            console.log(result);
        });
    }
}]);