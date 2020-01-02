import React, { useCallback } from "react";
import { useDispatch } from "react-redux";
import { Box, Button, Form, Heading, Text } from "grommet";
import { FormFieldLabel } from "components/forms";
import { actionCreators } from "store/auth";

export default function SignUpPage() {
	const dispatch = useDispatch();

	const onSubmit = useCallback(
		event => {
			const { callSign, firstName, lastName, password } = event.value;
			dispatch(
				actionCreators.signUp(callSign, firstName, lastName, password)
			);
		},
		[dispatch]
	);

	return (
		<Box align="center" pad="large">
			<Heading>Star Commander</Heading>
			<Form onSubmit={onSubmit}>
				<FormFieldLabel name="callSign" label="CallSign" required />
				<FormFieldLabel name="password" label="Password" required />
				<FormFieldLabel name="firstName" label="FirstName" required />
				<FormFieldLabel name="lastName" label="LastName" required />
				<Button type="submit" label="Sign up" primary />
				<Text
					margin={{ left: "small" }}
					size="small"
					color="status-critical"
				>
					* Required Field
				</Text>
			</Form>
		</Box>
	);
}
