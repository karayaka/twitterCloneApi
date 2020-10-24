using System;
using System.Collections.Generic;
using System.Text;

namespace twitterClone.Entity.Interfaces
{
    public interface IUnitOfWork
    {
        IAppBaseRepositoriys BaseRepositoriys { get; }

        IPostRepository PostRepository { get; }

        IAuthRepository AuthRepository { get; }

        ICommentRepositoriys CommentRepositoriys { get; }

        IWallRepository WallRepository { get; }

        ILikeAndDisLikeRepository LikeAndDisLikeRepository { get; }

       

        int SaveChange();

    }
}
