app.controller('contractorEditProfileController', ['$scope', 'dataService', function ($scope, db) {

    db.contractor.profile().success(function (result) {
        result.picture = { url: result.pictureUrl };
        result.loc = { formatted_address: result.location };
        result.primarySkills = [];
        result.secondarySkills = [];

        for (var x in result.skills) {
            if (result.skills[x].level == 'Primary') {
                result.primarySkills.push(result.skills[x]);
            } else {
                result.secondarySkills.push(result.skills[x]);
            }
        }

        $scope.record = result;
    });

    $scope.loadSkills= db.system.getSkills;

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
        record.skills = record.primarySkills.concat(record.secondarySkills);

        db.contractor.editProfile(record).success(function (result) {
            window.location = "#/profile";
        });
    }
}]);