app.controller('companyEditProfileController', ['$scope', 'dataService','$rootScope', function ($scope, db, $rootScope) {

    db.system.getIndustries().success(function(result) {
        $scope.industries = result;
    });

    db.system.getLocations().success(function (result) {
        $scope.locations = result;
    });

    db.company.get().success(function (result) {

        result.picture = { url: result.pictureUrl };
        result.loc = { formatted_address: result.location };
        $scope.record = result;
        angular.element('#txtemail').val(""); 
        angular.element('#txtDescription').val("");
    });

    $scope.refreshAddresses = function (address) {
      return db.system.searchLocations(address).then(function (response) {
            $scope.addresses = response.data.results;
        });
    };

    $scope.save = function (record) {
        $('input[type=text]').each(function () {
            $(this).val('');
        });
        if (record.picture) {
            record.pictureUrl = record.picture.url;
        }
        if (record.loc) {
            record.location = record.loc.formatted_address;
        }

        db.company.update(record).success(function (result) {
            window.location = "#/dashboard";
        });
    }

    $scope.orgType = db.system.orgType;

}]);