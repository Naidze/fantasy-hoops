import React from 'react';
import PropTypes from 'prop-types';
import moment from 'moment';
import Img from 'react-image';
import markdown from 'markdown';
import defaultPhoto from '../../../content/images/default.png';
import PostCardMenu from './PostCardMenu';

import './PostCard.css';


function PostCard(props) {
    const {user, post} = props;
    
    function handleRemove(post) {
        const {handleRemove} = props;
        handleRemove(post);
    }
    
    const canEdit = user => user && user.isAdmin;
    
    return (
        <article className="PostCard">
            <h2 className="PostCard__Title">
                {post.title}
            </h2>
            <div className="PostCard__Date">
                <span>{moment(post.createdAt).format("MMM D, YYYY")}</span>
            </div>
            <div className="PostCard__Author">
                <Img
                    className="PostCard__AuthorImage Avatar--round"
                    alt=""
                    src={[
                        `${process.env.REACT_APP_IMAGES_SERVER_NAME}/content/images/avatars/${post.author.avatarURL}.png`,
                        defaultPhoto
                    ]}
                    loader={<img height="50px" src={require('../../../content/images/imageLoader.gif')}
                                 alt="Loader"/>}
                />
                <span className="PostCard__AuthorName">{post.author.userName}</span>
                {canEdit(user) ? <PostCardMenu handleRemove={handleRemove} {...props} /> : null}
            </div>
            <div
                className="PostCard__Body"
                dangerouslySetInnerHTML={{__html: markdown.parse(post.body)}}
            />
        </article>
    );
}

PostCard.propTypes = {
    post: PropTypes.shape({
        title: PropTypes.string.isRequired,
        body: PropTypes.string.isRequired,
        author: PropTypes.shape({
            userName: PropTypes.string.isRequired,
            id: PropTypes.string.isRequired
        }).isRequired
    }).isRequired
};

export default PostCard;
