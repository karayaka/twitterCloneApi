using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using twiterClone.DAL.DataContext;
using twitterClone.Entity.Interfaces;

namespace twitterClone.Entity.Bussenes
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CloneDataContext context;
        IHttpContextAccessor httpContextAccessor;
        public UnitOfWork(CloneDataContext _context,IHttpContextAccessor _httpContextAccessor)
        {
            context = _context ?? throw new ArgumentNullException("context can not be null");
            httpContextAccessor = _httpContextAccessor;
        }
        //Obje Tanımları
        private IAppBaseRepositoriys _BaseRepositoriys;

        private IPostRepository _PostRepository;

        private IAuthRepository _AuthRepository;

        private ICommentRepositoriys _CommentRepositoriys;

        private ILikeAndDisLikeRepository _LikeAndDisLikeRepository;

        private IWallRepository _WallRepository;

        //Obje Tanımları Injection

        public IAppBaseRepositoriys BaseRepositoriys 
        {
            get => _BaseRepositoriys ??(_BaseRepositoriys=new AppBaseRepository(context, httpContextAccessor));
        }
        public IPostRepository PostRepository 
        {
            get => _PostRepository ?? (_PostRepository = new PostRepository(context, httpContextAccessor));
        }

        public IAuthRepository AuthRepository 
        {
            get => _AuthRepository ?? (_AuthRepository = new AuthRepository(context));
            
        }

        public ICommentRepositoriys CommentRepositoriys 
        {
            get => _CommentRepositoriys ?? (_CommentRepositoriys = new CommentRepositoriys(context, httpContextAccessor));
        }

        public ILikeAndDisLikeRepository LikeAndDisLikeRepository
        {
            get => _LikeAndDisLikeRepository ?? (_LikeAndDisLikeRepository = new LikeAndDisLikeRepository(context, httpContextAccessor));
        }

        public IWallRepository WallRepository
        {
            get => _WallRepository ?? (_WallRepository = new WallRepository(context, httpContextAccessor));
        }

       

        public int SaveChange()
        {
            try
            {
                return context.SaveChanges();
            }
            catch (Exception)
            {

                return -1;
            }
        }
    }
}
