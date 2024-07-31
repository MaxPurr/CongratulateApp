import formatDate from '../../helpers/formatDate';
import css from '../../css/birthday/BirthdayListItem.module.css'

function BirthdayListItem({ birthday }) {
    const { personId, personName, personSurname, personPhotoUrl, personAge, date, daysLeft } = birthday;
    const fullname = `${personName} ${personSurname}`;
    const formattedDate = formatDate(date);
    const daysLeftText = daysLeft == 0 ? "Сегодня" : `Осталось дней: ${daysLeft}`;
    return (
        <div className={css.birthday_container}>
            <img className={css.birthday_image} src={personPhotoUrl} alt={fullname}/>
            <div className={css.birthday_text_container}>
                <p className={css.person_name}>{fullname}</p>
                <p className={css.person_birthday}>{formattedDate}</p>
                <p className={css.birthday_info}>{daysLeftText}</p>
            </div>
        </div>
    );
}

export default BirthdayListItem;