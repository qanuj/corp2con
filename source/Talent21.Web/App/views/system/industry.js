app.controller('industryController', ['$scope', 'dataService', function ($scope, db) {
    
    function refreshRecord(page) {
        return db.system.getIndustries(page).success(function (result) {
            $scope.industries = result;
        });
    }

    $scope.save = function (record) {
        record.system = [];
        db.system.addIndustry(record).success(function (refreshRecord) {
            console.log(refreshRecord);
        });
    }

    $scope.update = function (record) {
        db.system.updateIndustry(record).success(refreshRecord);
    }

    $scope.update = function (i) {
        db.system.editIndustry(i).success(refreshRecord);
    };


    $scope.delete = function (i) {
        db.system.deleteIndustry(i).success(function (result) {
        });
    }

    $scope.toggle = function (i) {
        i.editMode = !i.editMode;
    };

    refreshRecord();

}]);