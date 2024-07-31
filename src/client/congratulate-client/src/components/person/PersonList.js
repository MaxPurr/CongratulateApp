import PersonListItem from './PersonListItem';
import css from '../../css/person/PersonList.module.css'

function PersonList({people, onDelete}) {
    return <>
        <ul className={css.person_list}>
            {people.map(person => (
                <li key={person.id}>
                    <PersonListItem person={person} onDelete={onDelete} />
                </li>
            ))}
        </ul>
    </>
}

export default PersonList;