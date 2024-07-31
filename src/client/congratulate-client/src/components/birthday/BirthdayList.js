import BirthdayListItem from './BirthdayListItem';
import css from '../../css/birthday/BirthdayList.module.css'

function BirthdayList({ birthdays }) {
    return <>
        <ul className={css.birthday_list}>
            {birthdays.map(birthday => (
                <li key={birthday.personId}>
                    <BirthdayListItem birthday={birthday} />
                </li>
            ))}
        </ul>
    </>
}

export default BirthdayList;