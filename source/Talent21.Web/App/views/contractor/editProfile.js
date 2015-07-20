app.controller('contractorEditProfileController', ['$scope', 'dataService', function ($scope, db) {

    db.contractor.profile().success(function (result) {
        result.picture = { url: result.pictureUrl };
        result.loc = { formatted_address: result.location };
        $scope.record = result;
    });

    db.system.getSkills().success(function (result) {
        $scope.skills = result;
    });

    $scope.refreshAddresses = function (address) {
        return db.system.searchLocations(address).then(function (response) {
            $scope.addresses = response.data.results;
        });
    };

    $scope.save = function (record) {
        if (record.cv) {
            record.profileUrl = record.cv.url;
        }
        if (record.picture) {
            record.pictureUrl = record.picture.url;
        }
        if (record.loc) {
            record.location = record.loc.formatted_address;
        }
        record.skill = '';
        for (var x in record.skills) {
            if (record.skill != '') record.skill += ',';
            record.skill += record.skills[x].text;
        }

        db.contractor.editProfile(record).success(function (result) {
            window.location = "#/profile";
        });
    }
}]);