app.controller('contractorEditProfileController', ['$scope', 'dataService', function ($scope, db) {

    db.contractor.profile().success(function (result) {
        result.picture = { url: result.pictureUrl };
        $scope.record = result;
    });

    db.system.getLocations().success(function (result) {
        $scope.cities = result;
        console.log(result);
    });

    db.system.getSkills().success(function (result) {
        $scope.skills = result;
    });

    $scope.save = function (record) {
        if (record.cv) {
            record.profileUrl = record.cv.url;
        }
        if (record.picture) {
            record.pictureUrl = record.picture.url;
        }
        db.contractor.editProfile(record).success(function (result) {
            console.log('Done');
        });
    }
}]);