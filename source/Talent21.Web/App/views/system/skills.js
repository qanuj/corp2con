app.controller('jobskillsController', ['$scope', 'dataService', function ($scope, db) {

    $scope.navigate = function (page) {
        refreshRecord();
        db.system.pagedSkill(page).success(function (result) {
            $scope.currentPage = page || 1;
            $scope.pages = Math.ceil(result.count / db.pageSize);
            $scope.records = result.items;
            console.log(result);
        });


        $scope.save = function (record) {
            $('input[type=text]').each(function () {
                $(this).val('');
            });
            db.system.addSkill(record).success(refreshRecord);
        }

        $scope.update = function (record) {
            db.system.editSkill(record).success(refreshRecord);
        };

        $scope.delete = function (record) {
            db.system.deleteSkill(record).success(refreshRecord);
        };

        $scope.toggle = function (record) {
            record.editMode = !record.editMode;
        };

        function refreshRecord() {
            return db.system.getSkills().success(function (result) {
                $scope.records = result;
            });
        }
    }
    $scope.navigate(params.page);
}]);

