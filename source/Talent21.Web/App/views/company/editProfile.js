app.controller('companyEditProfileController', ['$scope', 'dataService', function ($scope, db) {

    db.company.get().success(function (result) {
        result.picture = { url: result.pictureUrl };
        result.loc = { formatted_address: result.location };
        $scope.record = result;
    });

    //$scope.refreshAddresses = function (address) {
    //  return db.system.searchLocations(address).then(function (response) {
    //        $scope.addresses = response.data.results;
    //    });
    //};

    $scope.save = function (record) {
        
        if (record.picture) {
            record.pictureUrl = record.picture.url;
        }
        if (record.loc) {
            record.location = record.loc.formatted_address;
            $scope.industries = data;
        }

       
        db.company.update(record).success(function (result) {
            window.location = "#/profile";
        });
    }

    //db.industry.getIndustries().success(function (result) {
    //    $scope.industries = result.data;
    //});

}]);