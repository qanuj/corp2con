app.controller('contractorEditProfileController', ['$scope', 'dataService', function ($scope, db) {

    db.contractor.profile().success(function(result) {
        $scope.record = result;
    });

    db.system.getLocations().success(function (result) {
        $scope.cities = result;
        console.log(result);
    });

    db.system.getSkills().success(function (result) {
        $scope.skills = result;
    });

    $scope.save = function(record) {
        db.contractor.editProfile(record).success(function (result) {
            console.log('Done');
        });
    }
}]);