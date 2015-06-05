﻿using System;
namespace BusinessLogic.Repositories
{
    public interface IActiviteitRepository
    {
        System.Collections.Generic.IEnumerable<Models.OmgevingsBoek_Models.Activiteit> All();
        void Delete(Models.OmgevingsBoek_Models.Activiteit id);
        void DeleteSoft(Models.OmgevingsBoek_Models.Activiteit entityToDelete);
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.Activiteit> get50FromSortNameAZ(int from);
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.Activiteit> get50FromSortNameZA(int from);
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.Activiteit> get50FromSortPoiAZ(int from);
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.Activiteit> get50FromSortPoiZA(int from);
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.Activiteit> get50FromSortUserAZ(int from);
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.Activiteit> get50FromSortUserZA(int from);
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.Activiteit> getActiviteitenByPoiByUser50from(int from, string Owner, int PoiId);
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.Activiteit> getActiviteitenPerPoi(int id);
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.Activiteit> getActivitiesByUsername(string Username);
        Models.OmgevingsBoek_Models.Activiteit GetByID(object id);
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.Activiteit> getSharedActivitiesByBookId(int BoekId, string Username);
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.Activiteit> getSharedActivitiesByUsername(string Username);
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.Activiteit> getUserActiviteitenByUser50from(int from, string Owner, string Visitor);
    }
}
