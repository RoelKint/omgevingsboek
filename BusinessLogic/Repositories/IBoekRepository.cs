﻿using System;
namespace BusinessLogic.Repositories
{
    public interface IBoekRepository
    {
        void addUserToShareList(int Id, string Username, bool IsGedeeld);
        void Delete(Models.OmgevingsBoek_Models.Boek entityToDelete);
        void DeleteSoft(Models.OmgevingsBoek_Models.Boek entityToDelete);
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.Boek> get50FromSortDeletedAZ(int from, string search, bool DisplayDeleted);
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.Boek> get50FromSortDeletedZA(int from, string search, bool DisplayDeleted);
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.Boek> get50FromSortNameAZ(int from, string search, bool DisplayDeleted);
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.Boek> get50FromSortNameZA(int from, string search, bool DisplayDeleted);
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.Boek> get50FromSortUserAZ(int from, string search, bool DisplayDeleted);
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.Boek> get50FromSortUserZA(int from, string search, bool DisplayDeleted);
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.Boek> getBoekenByUser(string username);
        Models.OmgevingsBoek_Models.Boek GetByID(object id);
        Models.OmgevingsBoek_Models.Boek GetByIDAdmin(int id);
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.Boek> getSharedBoeken(string username);
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.Boek> getUserBoekByUser50from(int from, string Owner, string Visitor);
        Models.OmgevingsBoek_Models.Boek Insert(Models.OmgevingsBoek_Models.Boek entity);
        bool IsBoekAccessibleByUser(int BoekId, string Username);
        void Update(Models.OmgevingsBoek_Models.Boek entityToUpdate);
        void UpdateFoto(int BoekId, string foto);
    }
}
