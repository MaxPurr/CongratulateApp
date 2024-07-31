import { useState } from 'react';
import css from '../../css/common/SlidingSection.module.css'

function SlidingSection({title, children}) {
    const [isShowed, setIsShowed] = useState(false);

    const toggleIsShowed = () => {
        setIsShowed(!isShowed);
    }

    const getClassName = () => {
        return `${css.sliding_section_pane} ${isShowed ? css.sliding_section_pane_showed : ''}`;
    }

    return (
        <div className={css.sliding_section}>
            <button className={css.show_btn} type="button" onClick={toggleIsShowed}>
                {title}
            </button>
            <div className={getClassName()} >
                {children}
            </div>
        </div>
    );
}

export default SlidingSection;