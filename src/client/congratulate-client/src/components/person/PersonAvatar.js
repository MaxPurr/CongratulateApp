import css from '../../css/person/PersonAvatar.module.css'

function PersonAvatar({src, alt}) {
    return (
        <div className={css.person_image_wrapper}>
            <img className={css.person_image} src={src} alt={alt} />
        </div>
    );
}

export default PersonAvatar;