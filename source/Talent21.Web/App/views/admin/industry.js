app.controller('adminIndustryController', ['$scope', 'dataService', '$routeParams', function ($scope, db, params) {
    $scope.title = "Industries";

    $scope.navigate = function (page) {

        db.system.pagedIndustries(page).success(function (result) {
            $scope.currentPage = page || 1;
            $scope.pages = Math.ceil(result.count / db.pageSize);
            $scope.records = result.items;
        });

        $scope.save = function (record) {
            $('input[type=text]').each(function () {
                $(this).val('');
            });
            db.system.addIndustry(record).success($scope.navigate());
        }

        function refreshRecord() {
            return db.system.getIndustries().success(function (result) {
                $scope.records = result.items;
            });
        }

        $scope.update = function (record) {
            db.system.updateIndustry(record).success($scope.navigate(params.page));
        }

        $scope.delete = function (i) {
            db.system.deleteIndustry(i).success($scope.navigate(params.page));
        }

        $scope.toggle = function (i) {
            i.editMode = !i.editMode;
        };

    }

    $scope.navigate(params.page);
}]);