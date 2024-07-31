import formatDate from '../../helpers/formatDate';
import PersonAvatar from './PersonAvatar';
import css from '../../css/person/PersonListItem.module.css'

function PersonListItem({ person, onDelete }) {
    const { id, name, surname, dayOfBirth, photoUrl } = person;
    const fullname = `${name} ${surname}`;
    const formattedDayOfBirth = formatDate(dayOfBirth);

    async function handleDelete() {
        onDelete(id);
    }

    return <>
        <div className={css.person_container}>
            <div className={css.person_info_container}>
                <PersonAvatar src={photoUrl} alt={fullname} />
                <p className={css.person_name}>{fullname}</p>
                <p className={css.person_info}>{formattedDayOfBirth}</p>
            </div>
            <button className={css.person_delete_bth} type="button" onClick={handleDelete}>Удалить</button>
        </div>
    </>
}

export default PersonListItem;