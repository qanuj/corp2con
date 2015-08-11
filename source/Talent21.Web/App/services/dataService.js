app.factory('dataService', ['$http', '$q', function ($http, $q) {
    var v = 'api/v1/';
    var factory = {
        pageSize: 10,
        contractor: {},
        job: {},
        company: {},
        system: {}
    };

    function calculatePaging(page, pageSize) {
        pageSize = pageSize || factory.pageSize;
        var pg = "&$top=" + pageSize;
        if (page > 1) {
            pg += "&$skip=" + ((page - 1) * pageSize);
        }
        return pg;
    }
    function orderBy(order) {
        return '&$orderby=' + order;
    }

    factory.role = document.querySelector('html').dataset.role;

    //For Contractors
    factory.contractor.paged = function (page,pageSize) {
        return $http.get(v + 'contractor/paged?$inlinecount=allpages' + calculatePaging(page,pageSize));
    }

    factory.contractor.dashboard = function () {
        return $http.get(v + 'contractor/dashboard');
    }

    factory.contractor.all = function () {
        return $http.get(v + 'contractor/all');
    }

    factory.contractor.get = function (id) {
        return $http.get(v + 'contractor/profile' + (!!id ? '/' + id : ''));
    }

    factory.contractor.update = function (formData) {
        return $http.put(v + 'contractor/profile', formData);
    }

    factory.contractor.search = function (query, page, pageSize) {
        return $http.post(v + 'job/search?$inlinecount=allpages' + calculatePaging(page, pageSize) + orderBy('Id'), query);
    }
    
    factory.contractor.jobById = function (id) {
        return $http.get(v + 'contractor/job/' + id);
    }

    factory.contractor.ApplyToJob = function (id) {
        return $http.post(v + 'contractor/job/' + id + '/apply');
    }

    factory.contractor.applications = function (id) {
        return $http.put(v + '/job/application/' + id + '/apply');
    }
    
    //factory.contractor.getJobApplications = function () {
    //    return $http.get(v + 'contractor/job/application/');
    //}

    factory.contractor.getJobApplications = function (jobId,page,pageSize) {
        return $http.get(v + 'contractor/job/application?$inlinecount=allpages' + calculatePaging(page, pageSize));
    }

    factory.contractor.favorite = function (id) {
        return $http.put(v + 'contractor/job/application/' + id + '/favorite');
    }

   
    //factory.contractor.getFavoriteApplications = function () {
    //    return $http.get(v + 'contractor/job/application/favorite/all');
    //}

    factory.contractor.getFavoriteById = function (Id) {
        return $http.get(v + 'contractorjob/application/{id}/favorite?$filter=Id eq ' + Id);
    }
   

    factory.contractor.unfavorite = function (id) {
        return $http.delete(v + 'contractor/job/application/' + id + '/favorite');
    }

    factory.contractor.getFavorite = function (page, pageSize) {
        return $http.get(v + 'contractor/job/application/favorite/all?$orderby=Id desc' + calculatePaging(page, pageSize));
    }

    //factory.contractor.pagedFavorite = function (page, pageSize) {
    //    console.log('Page - ', page, ' Pagesize', pageSize);
    //    return $http.get(v + 'job/application/favorite/all/paged?$inlinecount=allpages' + calculatePaging(page, pageSize));
    //}

    factory.contractor.favoriteJob = function (formData) {
        return $http.put(v + 'contractor/job/application/{id}/favorite', formData);
    }

    factory.contractor.deleteFavoriteJob = function (record) {
        return $http.delete(v + 'contractor/job/application/{id}/favorite' + record.id);
    }

    factory.contractor.createSchedule = function (formData) {
        return $http.post(v + 'contractor/schedule', formData);
    }

    factory.contractor.editSchedule = function (formData) {
        return $http.put(v + 'contractor/schedule', formData);
    }
    factory.contractor.deleteSchedule = function (record) {
        return $http.delete(v + 'contractor/schedule/' + record.id);
    }

    factory.contractor.getSchedule = function (page,pageSize) {
        return $http.get(v + 'contractor/schedule/all?$orderby=Id desc' + calculatePaging(page,pageSize));
    }

    factory.contractor.getLatestJobs = function (skill, location, page, pageSize) {
        return $http.get(v + 'contractor/latest/jobs/' + skill + '/' + location + '?$orderby=Id desc' + calculatePaging(page, pageSize));
    }

    factory.contractor.topEmployers = function (skill, location, page, pageSize) {
        return $http.get(v + 'contractor/top/employers/' + skill + '/' + location + '?' + calculatePaging(page, pageSize));
    }

    //For companies

    factory.company.paged = function (page,pageSize) {
        return $http.get(v + 'company/paged?$inlinecount=allpages' + calculatePaging(page, pageSize));
    }

    factory.company.dashboard = function () {
        return $http.get(v + 'company/dashboard');
    }


    factory.company.all = function () {
        return $http.get(v + 'company/all');
    }

    factory.company.get = function (id) {
        return $http.get(v + 'company/profile' + (!!id ? '/' + id : ''));
    }

    factory.company.update = function (formData) {
        return $http.put(v + 'company/profile', formData);
    }

    factory.company.myJobs = function (page,pageSize) {
        return $http.get(v + 'company/job/paged?$inlinecount=allpages&$orderby=Id desc' + calculatePaging(page,pageSize));
    }

    factory.company.limitJobs = function (page,pageSize) {
        return $http.get(v + 'company/job/paged?$inlinecount=allpages'+calculatePaging(page,pageSize));
    }

    factory.company.search = function (query, page, pageSize, order) {
        return $http.post(v + 'company/search?$inlinecount=allpages' + calculatePaging(page, pageSize) + orderBy('Id'), query);
    }

    factory.company.getLatestProfiles = function(skill, location, page,pageSize) {
        return $http.get(v + 'company/latest/profiles/' + skill + '/' + location + '?$orderby=Id desc' + calculatePaging(page,pageSize));
    }

    factory.company.getTopProfiles = function (skill, location, page,pageSize) {
        return $http.get(v + 'company/top/profiles/' + skill + '/' + location + '?$orderby=Id desc' + calculatePaging(page,pageSize));
    }

    factory.company.getJobApplications = function (id) {
        return $http.get(v + 'company/job/' + id + '/applications/all');
    }

    factory.company.getJobApplication = function (id) {
        return $http.get(v + 'company/job/application/'+id);
    }

    factory.company.acceptContractor = function (id) {
        return $http.get(v + 'company/job/' + id + '/applications/all');
    }

    factory.company.getSchedule = function (id) {
        return $http.get(v + 'company/schedule/' + id);
    }

    //For Jobs

    factory.job.get = function (id) {
        return $http.get(v + 'company/job/' + id);
    }

    factory.job.paged = function (page,pageSize) {
        return $http.get(v + 'company/job/paged?$inlinecount=allpages$orderby=Id desc' + calculatePaging(page,pageSize));
    }

    factory.job.create = function (formData) {
        return $http.post(v + 'company/job', formData);
    }

    factory.job.update = function (record) {
        return $http.put(v + 'company/job', record);
    }

    factory.job.publish = function (id) { return $http.put(v + 'company/job/publish', { id: id }); }
    factory.job.unpublish = function (id) { return $http.put(v + 'company/job/unpublish', { id: id }); }
    factory.job.cancel = function (id) { return $http.put(v + 'company/job/cancel', { id: id }); }
    factory.job.revoke = function (id) { return $http.put(v + 'contractor/job/application/'+id+'/revoke'); }
    factory.job.delete = function (id) { return $http.delete(v + 'company/job/'+id); }

    factory.system.getSkills = function (q) {
        var uri = v + 'system/skill/all';
        if (q) uri += '?$filter=substringof(\'' + q + '\',Title)';
        return $http.get(uri);
    }

   factory.system.getLocations = function (q) {
        var uri = v + 'system/location/all';
        if (q) uri += '?$filter=substringof(\'' + q + '\',Title)';
        return $http.get(uri);
    }

   factory.system.pagedSkill = function (page, pageSize) {
       console.log('Page - ', page, ' Pagesize', pageSize);
       return $http.get(v + 'system/skill/paged?$inlinecount=allpages' + calculatePaging(page, pageSize));
   }

    factory.system.addSkill = function (record) {
        return $http.post(v + 'system/skill/create', record);
    }

    factory.system.editSkill = function (formData) {
        return $http.put(v + 'system/skill/update', formData);
    }

    factory.system.deleteSkill = function (record) {
        return $http.delete(v + 'system/skill/' + record.id);
    }

    factory.system.pagedIndustries = function (page, pageSize) {
        console.log('Page - ', page, ' Pagesize', pageSize);
        return $http.get(v + 'system/industry/paged?$inlinecount=allpages' + calculatePaging(page, pageSize));
    }

    factory.system.getIndustries = function (page,pageSize) {
        return $http.get(v + 'system/industry/all?$orderby=Id desc' + calculatePaging(page,pageSize));
    }

    factory.system.addIndustry = function (record) {
        return $http.post(v + 'system/industry/create', record);
    }
    factory.system.updateIndustry = function (record) {
        return $http.put(v + 'system/industry/update', record);
    }
    factory.system.deleteIndustry = function (record) {
        return $http.delete(v + 'system/industry/'+record.id);
    }

    factory.system.pagedFunctional = function (page, pageSize) {
        console.log( 'Page - ',page, ' Pagesize',pageSize);
        return $http.get(v + 'system/functional/paged?$inlinecount=allpages' + calculatePaging(page, pageSize));
    }

    factory.system.getFunctionals = function (page,pageSize) {
        return $http.get(v + 'system/functional/all?$orderby=Id desc' + calculatePaging(page,pageSize));
    }

    factory.system.addFunctional = function (record) {
        return $http.post(v + 'system/functional/create', record);
    }
    factory.system.updateFunctional = function (record) {
        return $http.put(v + 'system/functional/update', record);
    }
    factory.system.deleteFunctional = function (record) {
        return $http.delete(v + 'system/functional/'+record.id);
    }

    factory.system.searchLocations=function(address) {
        var params = { address: address, sensor: false };
        return common.$http.get(
          '//maps.googleapis.com/maps/api/geocode/json',
          { params: params }
        );
    }

    factory.system.enums = function (name) {
        var deferred = $q.defer();
        var that = this;
        if (!that.enumsRows) {
            $http.get(v + 'system/enums').success(function(enums) {
                that.enumsRows = enums;
                deferred.resolve(that.enumsRows[name]);
            });
        } else {
            return deferred.resolve(that.enumsRows[name]);
        }
        return deferred.promise;
    }

    return factory;
}]);