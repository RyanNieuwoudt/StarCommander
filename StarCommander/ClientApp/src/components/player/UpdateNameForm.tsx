import React, { useCallback } from "react";
import { useDispatch, useSelector } from "react-redux";
import { Button, Form } from "grommet";
import { FormFieldLabel, RequiredFieldLabel } from "components/forms";
import { player } from "selectors";
import { actionCreators } from "store/auth";

export default function UpdateNameForm() {
	const dispatch = useDispatch();

	const onSubmit = useCallback(
		(event) => {
			const { firstName, lastName } = event.value;
			dispatch(actionCreators.updateName(firstName, lastName));
		},
		[dispatch]
	);

	const { firstName, lastName } = useSelector(player) || {
		firstName: "",
		lastName: "",
	};

	return (
		<Form onSubmit={onSubmit}>
			<FormFieldLabel
				name="firstName"
				label="FirstName"
				value={firstName}
				required
			/>
			<FormFieldLabel
				name="lastName"
				label="LastName"
				value={lastName}
				required
			/>
			<Button type="submit" label="Update" primary />
			<RequiredFieldLabel />
		</Form>
	);
}
