app.controller('industryController', ['$scope', 'dataService', function ($scope, db) {

    function refreshRecord(page) {
        return db.system.getIndustries(page).success(function (result) {
            $scope.industries = result;
        });
    }

    $scope.save = function (record) {
        db.system.addIndustry(record).success(refreshRecord);
    }

    $scope.update = function (record) {
        db.system.updateIndustry(record).success(refreshRecord);
    }

    $scope.delete = function (record) {
        db.system.deleteIndustry(record).success(refreshRecord);
    }

    $scope.toggle = function (i) {
        i.editMode = !i.editMode;
    };

    refreshRecord();

}]);